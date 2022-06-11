// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeCommandHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Events;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands
{
    internal class PermitTypeCommandHandler :
        IRequestHandler<CreatePermitTypeCommand, Result<Guid>>,
        IRequestHandler<UpdatePermitTypeCommand, Result<Guid>>,
        IRequestHandler<RemovePermitTypeCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IWorkflowDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PermitTypeCommandHandler> _localizer;
        private readonly ILogger<PermitTypeCommandHandler> _logger;
        private readonly IPermitTypeService _permitTypeService;
        private readonly IPermitService _permitService;

        public PermitTypeCommandHandler(
            IWorkflowDbContext context,
            IMapper mapper,
            IStringLocalizer<PermitTypeCommandHandler> localizer,
            ILogger<PermitTypeCommandHandler> logger,
            IPermitTypeService permitTypeService,
            IPermitService permitService,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _logger = logger;
            _permitTypeService = permitTypeService;
            _permitService = permitService;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(CreatePermitTypeCommand command, CancellationToken cancellationToken)
        {
            var permitType = _mapper.Map<PermitType>(command);
            permitType.CodeInternal.ToUpper();
            try
            {
                permitType.AddDomainEvent(new PermitTypeRegisteredEvent(permitType));
                await _context.PermitTypes.AddAsync(permitType, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(permitType.Id, _localizer["PermitType Saved"]);
            }
            catch (Exception)
            {
                throw new PermitTypeCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(UpdatePermitTypeCommand command, CancellationToken cancellationToken)
        {
            var permitType = await _context.PermitTypes.Where(x => x.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            _ = permitType ?? throw new EmployeeNotFoundException(_localizer, command.Id);

            permitType.Name = command.Name ?? permitType.Name;
            permitType.CodeInternal = command.CodeInternal.ToUpper() ?? permitType.CodeInternal;
            permitType.Description = command.Description ?? permitType.Description;
            permitType.IsEnabled = command.IsEnabled || permitType.IsEnabled;

            try
            {
                permitType.AddDomainEvent(new PermitTypeUpdatedEvent(permitType));
                _context.PermitTypes.Update(permitType);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, PermitType>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(permitType.Id, _localizer["PermitType Updated"]);
            }
            catch (Exception)
            {
                throw new PermitTypeCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(RemovePermitTypeCommand command, CancellationToken cancellationToken)
        {
            var data = await _context.PermitTypes.Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            _ = data ?? throw new PermitTypeNotFoundException(_localizer, command.Id);
            if (!await _permitService.IsPermitTypeUseAsync(data.Id))
            {
                try
                {
                    data.AddDomainEvent(new PermitTypeRemovedEvent(data.Id));
                    _context.PermitTypes.Remove(data);
                    await _context.SaveChangesAsync(cancellationToken);
                    await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, PermitType>(command.Id), cancellationToken);
                    return await Result<Guid>.SuccessAsync(data.Id, _localizer["PermitType Deleted"]);
                }
                catch (Exception)
                {
                    throw new PermitTypeCustomException(_localizer, null);
                }
            }
            else
            {
                _logger.LogWarning(string.Format(_localizer["Not allowed delete permittype Id:{0}. Has associated permits"], data.Id));
                return await Result<Guid>.FailAsync(data.Id, _localizer["Deletion Not Allowed PermitType"]);
            }
        }
    }
}
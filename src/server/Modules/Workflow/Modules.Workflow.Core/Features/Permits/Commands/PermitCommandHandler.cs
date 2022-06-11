// --------------------------------------------------------------------------------------------------
// <copyright file="PermitCommandHandler.cs" company="HureIT">
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
using HureIT.Modules.Workflow.Core.Features.Elevators.Events;
using HureIT.Modules.Workflow.Core.Features.Permits.Events;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Commands
{
    internal class PermitCommandHandler :
        IRequestHandler<AssignPermitCommand, Result<Guid>>,
        IRequestHandler<UpdatePermitCommand, Result<Guid>>,
        IRequestHandler<RemovePermitCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IWorkflowDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PermitCommandHandler> _localizer;

        public PermitCommandHandler(
            IDistributedCache cache,
            IWorkflowDbContext context,
            IMapper mapper,
            IStringLocalizer<PermitCommandHandler> localizer)
        {
            _cache = cache;
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<Guid>> Handle(AssignPermitCommand command, CancellationToken cancellationToken)
        {
            var permit = _mapper.Map<Permit>(command);
            try
            {
                permit.AddDomainEvent(new PermitAssignedEvent(permit));
                await _context.Permits.AddAsync(permit, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(permit.Id, _localizer["Permission Saved"]);
            }
            catch (Exception)
            {
                throw new PermitCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(UpdatePermitCommand command, CancellationToken cancellationToken)
        {
            var permit = await _context.Permits.Where(x => x.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            _ = permit ?? throw new PermitNotFoundException(_localizer, command.Id);

            permit.EmployeeId = command.EmployeeId != Guid.Empty ? command.EmployeeId : permit.EmployeeId;
            permit.PermitTypeId = command.PermitTypeId != Guid.Empty ? command.PermitTypeId : permit.PermitTypeId;
            permit.StartDatePermit = command.StartDatePermit;
            permit.EndDatePermit = command.EndDatePermit;

            try
            {
                permit.AddDomainEvent(new PermitUpdatedEvent(permit));
                _context.Permits.Update(permit);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Permit>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(permit.Id, _localizer["Permission Updated"]);
            }
            catch (Exception)
            {
                throw new PermitCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(RemovePermitCommand command, CancellationToken cancellationToken)
        {
            var permit = await _context.Permits.AsNoTracking().Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            _ = permit ?? throw new PermitNotFoundException(_localizer, command.Id);
            try
            {
                permit.AddDomainEvent(new PermitRemovedEvent(permit.Id));
                _context.Permits.Remove(permit);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Permit>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(permit.Id, _localizer["Permission Deleted"]);
            }
            catch (Exception)
            {
                throw new PermitCustomException(_localizer, null);
            }
        }
    }
}
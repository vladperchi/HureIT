// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeCommandHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Modules.Workflow.Core.Features.Employees.Events;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Commands
{
    internal class EmployeeCommandHandler :
        IRequestHandler<RegisterEmployeeCommand, Result<Guid>>,
        IRequestHandler<UpdateEmployeeCommand, Result<Guid>>,
        IRequestHandler<RemoveEmployeeCommand, Result<Guid>>
    {
        private readonly IDistributedCache _cache;
        private readonly IWorkflowDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPermitService _permitService;
        private readonly IStringLocalizer<EmployeeCommandHandler> _localizer;
        private readonly ILogger<EmployeeCommandHandler> _logger;

        public EmployeeCommandHandler(
            IWorkflowDbContext context,
            IMapper mapper,
            IPermitService permitService,
            IStringLocalizer<EmployeeCommandHandler> localizer,
            ILogger<EmployeeCommandHandler> logger,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _permitService = permitService;
            _localizer = localizer;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Result<Guid>> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
{
            var employee = _mapper.Map<Employee>(command);
            employee.ImageUrl = StringKeys.UserImageGhost;
            employee.AdmissionDate = DateTime.Now;
            try
            {
                employee.AddDomainEvent(new EmployeeRegisteredEvent(employee));
                await _context.Employees.AddAsync(employee, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<Guid>.SuccessAsync(employee.Id, _localizer["Employee Registered"]);
            }
            catch (Exception)
            {
                throw new EmployeeCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.Where(x => x.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            _ = employee ?? throw new EmployeeNotFoundException(_localizer, command.Id);

            employee.FirstName = command.FirstName ?? employee.FirstName;
            employee.LastName = command.LastName ?? employee.LastName;
            employee.PhoneNumber = command.PhoneNumber ?? employee.PhoneNumber;
            employee.Birthday = command.Birthday;
            employee.Gender = command.Gender ?? employee.Gender;
            employee.IsActive = command.IsActive || employee.IsActive;

            try
            {
                employee.AddDomainEvent(new EmployeeUpdatedEvent(employee));
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync(cancellationToken);
                await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Employee>(command.Id), cancellationToken);
                return await Result<Guid>.SuccessAsync(employee.Id, _localizer["Employee Updated"]);
            }
            catch (Exception)
            {
                throw new EmployeeCustomException(_localizer, null);
            }
        }

        public async Task<Result<Guid>> Handle(RemoveEmployeeCommand command, CancellationToken cancellationToken)
        {
            var data = await _context.Employees.Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
            _ = data ?? throw new EmployeeNotFoundException(_localizer, command.Id);
            if (!await _permitService.IsEmployeeUsedAsync(data.Id))
            {
                try
                {
                    data.AddDomainEvent(new EmployeeRemovedEvent(data.Id));
                    _context.Employees.Remove(data);
                    await _context.SaveChangesAsync(cancellationToken);
                    await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Employee>(command.Id), cancellationToken);
                    return await Result<Guid>.SuccessAsync(data.Id, _localizer["Employee Deleted"]);
                }
                catch (Exception)
                {
                    throw new EmployeeCustomException(_localizer, null);
                }
            }
            else
            {
                _logger.LogWarning(string.Format(_localizer["Not allowed delete employee Id:{0}. Has associated permits"], data.Id));
                return await Result<Guid>.FailAsync(data.Id, _localizer["Deletion not allowed employee"]);
            }
        }
    }
}
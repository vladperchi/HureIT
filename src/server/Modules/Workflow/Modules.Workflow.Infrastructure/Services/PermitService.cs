// --------------------------------------------------------------------------------------------------
// <copyright file="PermitService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Shared.Core.Interfaces.Serialization.Serializer;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Workflow.Infrastructure.Services
{
    public class PermitService : IPermitService
    {
        private readonly IWorkflowDbContext _context;
        private readonly ILogger<PermitService> _logger;
        private readonly IMapper _mapper;
        private readonly IJsonSerializer _json;
        private readonly IStringLocalizer<PermitService> _localizer;

        public PermitService(
            IWorkflowDbContext context,
            ILogger<PermitService> logger,
            IJsonSerializer json,
            IStringLocalizer<PermitService> localizer,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _json = json;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<bool> IsEmployeeUsedAsync(Guid id) =>
            await _context.Permits.AnyAsync(x => x.EmployeeId == id);

        public async Task<bool> IsPermitTypeUseAsync(Guid id) =>
            await _context.Permits.AnyAsync(x => x.PermitTypeId == id);

        public Task<bool> IsOverlappingDatesAsync(Guid employeeId, DateTime startDate, DateTime endDate)
        {
            var employee = _context.Employees.AsNoTracking().Where(x => x.Id == employeeId).FirstOrDefault();
            _ = employee ?? throw new EmployeeNotFoundException(_localizer, employeeId);
            var permits = _context.Permits.AsNoTracking().Where(x => x.EmployeeId.Equals(employee.Id)).ToList();
            return Task.FromResult(permits.Any(x => ContainsDateInRange(x, startDate, endDate)));
        }

        public Task<bool> IsPermitsGreaterThan30Days(DateTime startDate, DateTime endDate) =>
            Task.FromResult((endDate.Date - startDate.Date).Days > 30);

        public async Task<Result<GetEmployeeByIdResponse>> GetAssignedPermitsByEmployeeAsycn(Guid id)
        {
            var employee = await _context.Employees.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetEmployeeByIdResponse>(employee);
            var permits = await GetAllPermitsByEmployee(employee.Id);
            _ = permits ?? throw new PermitListEmptyException(_localizer);
            result.TotalPermits = permits.Count();
            result.PermitList = permits.ToList();
            return await Result<GetEmployeeByIdResponse>.SuccessAsync(result);
        }

        private async Task<IEnumerable<string>> GetAllPermitsByEmployee(Guid id)
        {
            var permits = await _context.Permits.AsNoTracking()
                .Include(x => x.PermitType)
                .Where(x => x.EmployeeId.Equals(id))
                .OrderByDescending(x => x.StartDatePermit).ToListAsync();
            _ = permits ?? throw new PermitListEmptyException(_localizer);
            return permits.Select(x => $"{x?.PermitType?.Name} ({x?.PermitType?.CodeInternal})" ?? string.Empty);
        }

        private bool ContainsDateInRange(Permit permit, DateTime startDate, DateTime endDate)
        {
            var permitDates = Enumerable.Range(0, 1 + permit.EndDatePermit.Subtract(permit.StartDatePermit).Days)
                  .Select(offset => permit.StartDatePermit.Date.AddDays(offset))
                  .ToArray();
            var newPermitDates = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.Date.AddDays(offset))
                .ToArray();
            return newPermitDates.Any(x => permitDates.Contains(x));
        }

        public async Task<Result<GetEmployeesWithPermitsResponse>> GetNumberPermitsByEmployeeAsycn(Guid id)
        {
            var employee = await _context.Employees.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetEmployeesWithPermitsResponse>(employee);
            var permits = await GetAllPermitsByEmployee(employee.Id);
            _ = permits ?? throw new PermitListEmptyException(_localizer);
            result.TotalPermits = permits.Count();
            result.PermitList = permits.ToList();
            return await Result<GetEmployeesWithPermitsResponse>.SuccessAsync(result);
        }

        public Task<Result<Guid>> ChangeStatusEmployeeAsync(Guid employeeId, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
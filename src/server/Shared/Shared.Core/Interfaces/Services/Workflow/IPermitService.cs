// --------------------------------------------------------------------------------------------------
// <copyright file="IPermitService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Employees;

namespace HureIT.Shared.Core.Interfaces.Services.Workflow
{
    public interface IPermitService
    {
        Task<bool> IsEmployeeUsedAsync(Guid employeeId);

        Task<bool> IsPermitTypeUseAsync(Guid permitTypeId);

        Task<bool> IsOverlappingDatesAsync(Guid employeeId, DateTime startDate, DateTime endDate);

        Task<Result<GetEmployeeByIdResponse>> GetAssignedPermitsByEmployeeAsycn(Guid id);

        Task<bool> IsPermitsGreaterThan30Days(DateTime startDate, DateTime endDate);

        Task<Result<Guid>> ChangeStatusEmployeeAsync(Guid employeeId, bool status);

        Task<Result<GetEmployeesWithPermitsResponse>> GetNumberPermitsByEmployeeAsycn(Guid id);
    }
}
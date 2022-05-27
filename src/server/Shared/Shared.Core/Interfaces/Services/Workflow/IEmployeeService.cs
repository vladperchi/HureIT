// --------------------------------------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="HureIT">
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
    public interface IEmployeeService
    {
        Task<Result<GetEmployeeByIdResponse>> GetDetailsAsync(Guid id);

        Task<Result<string>> GetPictureAsync(Guid id);

        Task<Result<Guid>> RemoveAsync(Guid id);

        Task<int> GetCountAsync();
    }
}
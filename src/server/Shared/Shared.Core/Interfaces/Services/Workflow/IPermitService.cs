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

namespace HureIT.Shared.Core.Interfaces.Services.Workflow
{
    public interface IPermitService
    {
        Task<Result<Guid>> ChangeStatusEmployeeAsync(Guid employeeId, bool status);

        Task<bool> IsEmployeeUsed(Guid employeeId);

        Task<bool> IsPermitTypeUsed(Guid permitTypeId);
    }
}
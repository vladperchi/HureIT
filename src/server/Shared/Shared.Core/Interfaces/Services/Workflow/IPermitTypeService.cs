// --------------------------------------------------------------------------------------------------
// <copyright file="IPermitTypeService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.PermitTypes;

namespace HureIT.Shared.Core.Interfaces.Services.Flow
{
    public interface IPermitTypeService
    {
        Task<Result<GetPermitTypeByIdResponse>> GetDetailsAsync(Guid id);

        Task<Result<Guid>> RemoveAsync(Guid id);

        Task<string> GenerateFileName(int length);

        Task<int> GetCountAsync();
    }
}
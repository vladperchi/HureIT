// --------------------------------------------------------------------------------------------------
// <copyright file="IStatsService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Dashboard;

namespace HureIT.Shared.Core.Interfaces.Services.Dashboard
{
    public interface IStatsService
    {
        Task<IResult<GetStatsDataResponse>> GetStatsDataAsync();
    }
}
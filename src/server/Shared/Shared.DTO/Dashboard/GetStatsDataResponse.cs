// --------------------------------------------------------------------------------------------------
// <copyright file="GetStatsDataResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace HureIT.Shared.DTO.Dashboard
{
    public class GetStatsDataResponse
    {
        public int EmployeeCount { get; set; }

        public int PermitTypeCount { get; set; }

        public int PermitCount { get; set; }

        public int UserCount { get; set; }

        public int RoleCount { get; set; }

        public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    }

    public class ChartSeries
    {
        public ChartSeries()
        {
        }

        public string Name { get; set; }

        public double[] Data { get; set; }
    }
}
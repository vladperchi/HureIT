// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedEmployeeFilter.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.DTO.Filters;

namespace HureIT.Shared.DTO.Workflow.Employees
{
    public class PaginatedEmployeeFilter : PaginatedFilter
    {
        public string SearchString { get; set; }
    }
}
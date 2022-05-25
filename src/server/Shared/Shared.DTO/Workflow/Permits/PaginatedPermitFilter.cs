// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedPermitFilter.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.DTO.Filters;

namespace HureIT.Shared.DTO.Workflow.Permits
{
    public class PaginatedPermitFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid[] EmployeeIds { get; set; }

        public Guid[] PermitTypesIds { get; set; }
    }
}
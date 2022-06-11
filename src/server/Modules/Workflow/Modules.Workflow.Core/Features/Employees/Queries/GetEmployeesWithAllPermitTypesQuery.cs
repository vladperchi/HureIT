// --------------------------------------------------------------------------------------------------
// <copyright file="GetEmployeesWithAllPermitTypesQuery.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Employees;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Queries
{
    public class GetEmployeesWithAllPermitTypesQuery : IRequest<PaginatedResult<GetEmployeesWithAllPermitTypeResponse>>
    {
        public string SearchString { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }
    }
}
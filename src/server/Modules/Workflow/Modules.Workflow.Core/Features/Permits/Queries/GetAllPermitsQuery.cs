// --------------------------------------------------------------------------------------------------
// <copyright file="GetAllPermitsQuery.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Permits;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Queries
{
    public class GetAllPermitsQuery : IRequest<PaginatedResult<GetAllPermitsResponse>>
    {
        public string SearchString { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public Guid[] EmployeeIds { get; private set; }

        public Guid[] PermitTypesIds { get; private set; }
    }
}
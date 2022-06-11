// --------------------------------------------------------------------------------------------------
// <copyright file="GetEmployeeImageQuery.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Wrapper;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Queries
{
    public class GetEmployeeImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; }

        public GetEmployeeImageQuery(Guid id)
        {
            Id = id;
        }
    }
}
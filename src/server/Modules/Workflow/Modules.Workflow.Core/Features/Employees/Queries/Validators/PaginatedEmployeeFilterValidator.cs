// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedEmployeeFilterValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Features.Queries.Validators;
using HureIT.Shared.DTO.Workflow.Employees;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Queries.Validators
{
    public class PaginatedEmployeeFilterValidator : PaginatedFilterValidator<Guid, Employee, PaginatedEmployeeFilter>
    {
        /// <summary>
        /// You can override the validation rules here.
        /// </summary>
        public PaginatedEmployeeFilterValidator(IStringLocalizer<PaginatedEmployeeFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}
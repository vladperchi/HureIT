// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedPermitTypeFilterValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Features.Queries.Validators;
using HureIT.Shared.DTO.Workflow.PermitTypes;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries.Validators
{
    public class PaginatedPermitTypeFilterValidator : PaginatedFilterValidator<Guid, PermitType, PaginatedPermitTypeFilter>
    {
        /// <summary>
        /// You can override the validation rules here.
        /// </summary>
        public PaginatedPermitTypeFilterValidator(IStringLocalizer<PaginatedPermitTypeFilterValidator> localizer)
            : base(localizer)
        {
        }
    }
}
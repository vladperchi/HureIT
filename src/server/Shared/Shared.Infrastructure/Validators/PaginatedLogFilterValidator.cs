// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedLogFilterValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Logging;
using HureIT.Shared.DTO.Identity.Logging;
using HureIT.Shared.Core.Features.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace HureIT.Shared.Infrastructure.Validators
{
    public class PaginatedLogFilterValidator : PaginatedFilterValidator<Guid, EventLog, PaginatedLogFilter>
    {
        public PaginatedLogFilterValidator(IStringLocalizer<PaginatedLogFilterValidator> localizer)
            : base(localizer)
        {
            // Here you can override the validation rules
        }
    }
}
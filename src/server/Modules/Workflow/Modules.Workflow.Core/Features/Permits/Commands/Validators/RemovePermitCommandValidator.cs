// --------------------------------------------------------------------------------------------------
// <copyright file="RemovePermitCommandValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Commands.Validators
{
    public class RemovePermitCommandValidator : AbstractValidator<RemovePermitCommand>
    {
        public RemovePermitCommandValidator(IStringLocalizer<RemovePermitCommandValidator> localizer)
        {
            RuleFor(x => x.Id).NotNull()
               .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);
        }
    }
}
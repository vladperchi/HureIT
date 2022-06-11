// --------------------------------------------------------------------------------------------------
// <copyright file="RemovePermitTypeCommandValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands.Validators
{
    public class RemovePermitTypeCommandValidator : AbstractValidator<RemovePermitTypeCommand>
    {
        public RemovePermitTypeCommandValidator(IStringLocalizer<RemovePermitTypeCommandValidator> localizer)
        {
            RuleFor(x => x.Id).NotNull()
               .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);
        }
    }
}
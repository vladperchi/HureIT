// --------------------------------------------------------------------------------------------------
// <copyright file="UpdatePermitTypeCommandValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands.Validators
{
    public class UpdatePermitTypeCommandValidator : AbstractValidator<UpdatePermitTypeCommand>
    {
        public UpdatePermitTypeCommandValidator(
            IPermitTypeService permitTypeService,
            IValidatorService validatorService,
            IStringLocalizer<UpdatePermitTypeCommandValidator> localizer)
        {
            RuleFor(x => x.Id).NotNull()
               .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);

            RuleFor(x => x.Name)
               .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
               .MaximumLength(100).WithMessage(localizer["{PropertyName} can have a maximum of 100 characters."])
               .NotEqual(x => x.Description).WithMessage(localizer["{PropertyName} cannot be equal to description."])
               .NotEqual(x => x.CodeInternal).WithMessage(localizer["{PropertyName} cannot be equal to code internal."])
               .MustAsync(async (name, _) => await validatorService.IsOnlyLetterAndSpace(name))
                   .WithMessage(localizer["{PropertyName} should be all letters."])
               .MustAsync(async (name, _) => !await permitTypeService.IsNameUnique(name))
                   .WithMessage((_, name) => string.Format(localizer["Permit Type name {0} is already exists."], name));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .NotEqual(x => x.Name).WithMessage(localizer["{PropertyName} cannot be equal to Name."])
                .NotEqual(x => x.CodeInternal).WithMessage(localizer["{PropertyName} cannot be equal to code internal."])
                .MustAsync(async (description, _) => !await permitTypeService.ExistsWithDescription(description))
                    .WithMessage(localizer["{PropertyName} with the same description already exists."]);

            RuleFor(x => x.CodeInternal)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MaximumLength(6).WithMessage(localizer["{PropertyName} must have maximu 6 characters."])
                .NotEqual(x => x.Name).WithMessage(localizer["{PropertyName} cannot be equal to Name."])
                .NotEqual(x => x.Description).WithMessage(localizer["{PropertyName} cannot be equal to description."])
                .MustAsync(async (code, _) => await validatorService.IsLetterOrDigit(code))
                    .WithMessage(localizer["{PropertyName} must be only letters and numbers."])
                .MustAsync(async (code, _) => !await permitTypeService.ExistsWithCode(code))
                    .WithMessage((_, code) => string.Format(localizer["Code Internal {0} is already taken."], code));
        }
    }
}
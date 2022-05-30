// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateUserRequestValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;

using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.DTO.Identity.Users;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Core.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator(
            IValidatorService validatorService,
            IStringLocalizer<UpdateUserRequestValidator> localizer)
        {
            RuleFor(x => x.Id)
                  .NotEqual(string.Empty).WithMessage(_ => localizer["{PropertyName} must not be empty."]);

            RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
            .EmailAddress().WithMessage(localizer["{PropertyName} must be a valid email accounts."]);

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MaximumLength(100).WithMessage(localizer["{PropertyName} can have a maximum of 100 characters."])
                .NotEqual(x => x.LastName).WithMessage(localizer["{PropertyName} cannot be equal to LastName."])
                .MustAsync(async (firstname, _) => await validatorService.IsOnlyLetterAndSpace(firstname))
                    .WithMessage(localizer["{PropertyName} should be all letters."]);

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MaximumLength(100).WithMessage(localizer["{PropertyName} can have a maximum of 100 characters."])
                .NotEqual(x => x.FirstName).WithMessage(localizer["{PropertyName} cannot be equal to FirstName."])
                .MustAsync(async (firstname, _) => await validatorService.IsOnlyLetterAndSpace(firstname))
                    .WithMessage(localizer["{PropertyName} should be all letters."]);
        }
    }
}
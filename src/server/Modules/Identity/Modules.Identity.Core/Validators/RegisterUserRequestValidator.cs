// --------------------------------------------------------------------------------------------------
// <copyright file="RegisterUserRequestValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentValidation;
using HureIT.Modules.Identity.Core.Abstractions;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.DTO.Identity.Users;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Core.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterUserRequestValidator(
            IUserService userService,
            IValidatorService validatorService,
            IStringLocalizer<RegisterUserRequestValidator> localizer)
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .EmailAddress().WithMessage(localizer["{PropertyName} must be a valid email accounts."])
                .MustAsync(async (email, _) => !await userService.ExistsWithEmailAsync(email))
                    .WithMessage((_, email) => string.Format(localizer["Email {0} is already registered."], email));

            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MinimumLength(8)
                .MustAsync(async (name, _) => !await userService.ExistsWithNameAsync(name))
                    .WithMessage((_, name) => string.Format(localizer["Username {0} is already taken."], name));

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
                .MustAsync(async (lastname, _) => await validatorService.IsOnlyLetterAndSpace(lastname))
                    .WithMessage(localizer["{PropertyName} should be all letters."]);

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MinimumLength(8).WithMessage(localizer["{PropertyName} minimum 8 characters."]);

            RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .Equal(x => x.Password).WithMessage(localizer["{PropertyName} do not match."]);
        }
    }
}
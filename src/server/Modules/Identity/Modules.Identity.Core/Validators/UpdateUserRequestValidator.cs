// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateUserRequestValidator.cs" company="HureIT">
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
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator(
            IUserService userService,
            IValidatorService validatorService,
            IStringLocalizer<UpdateUserRequestValidator> localizer)
        {
            RuleFor(x => x.Id).NotNull()
                .NotEqual(string.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);

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

            RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
               .Length(8, 16).WithMessage(localizer["{PropertyName} must have between 8 and  16 characters."])
               .MustAsync(async (phone, _) => await validatorService.IsOnlyNumber(phone))
                    .WithMessage(localizer["{PropertyName} should be all numbers."])
               .MustAsync(async (phone, _) => !await userService.ExistsWithPhoneNumberAsync(phone!))
                   .WithMessage((_, phone) => string.Format(localizer["Phone number {0} is already registered."], phone))
                   .Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));
        }
    }
}
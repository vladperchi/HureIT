// --------------------------------------------------------------------------------------------------
// <copyright file="ForgotPasswordRequestValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentValidation;
using HureIT.Shared.DTO.Identity.Users;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Core.Validators
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator(
            IStringLocalizer<ForgotPasswordRequestValidator> localizer)
        {
            RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .EmailAddress().WithMessage(localizer["{PropertyName} must be a valid email accounts."]);
        }
    }
}
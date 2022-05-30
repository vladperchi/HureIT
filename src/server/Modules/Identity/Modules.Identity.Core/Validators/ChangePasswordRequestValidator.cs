// --------------------------------------------------------------------------------------------------
// <copyright file="ChangePasswordRequestValidator.cs" company="HureIT">
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
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator(IStringLocalizer<ChangePasswordRequestValidator> localizer)
        {
            RuleFor(p => p.CurrentPassword)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."]);
            RuleFor(p => p.NewPassword)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."]);
            RuleFor(p => p.ConfirmNewPassword)
                .NotEqual(p => p.NewPassword).WithMessage(localizer["{PropertyName} do not match."]);
        }
    }
}
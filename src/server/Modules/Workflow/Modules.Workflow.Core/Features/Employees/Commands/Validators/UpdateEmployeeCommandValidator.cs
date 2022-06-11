// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateEmployeeCommandValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Commands.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator(
            IEmployeeService employeeService,
            IValidatorService validatorService,
            IStringLocalizer<UpdateEmployeeCommandValidator> localizer)
        {
            RuleFor(x => x.Id).NotNull()
                  .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                  .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                  .MaximumLength(50).WithMessage(localizer["{PropertyName} can have a maximum of 50 characters."])
                  .NotEqual(x => x.LastName).WithMessage(localizer["{PropertyName} cannot be equal to lastname."])
                  .MustAsync(async (firstname, _) => await validatorService.IsOnlyLetterAndSpace(firstname))
                      .WithMessage(localizer["{PropertyName} should be all letters."]);

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .MaximumLength(50).WithMessage(localizer["{PropertyName} can have a maximum of 50 characters."])
                .NotEqual(x => x.FirstName).WithMessage(localizer["{PropertyName} cannot be equal to firstname."])
                .MustAsync(async (lastname, _) => await validatorService.IsOnlyLetterAndSpace(lastname))
                    .WithMessage(localizer["{PropertyName} should be all letters."]);

            RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .Length(8, 16).WithMessage(localizer["{PropertyName} must have between 8 and  16 characters."])
                .MustAsync(async (phone, _) => await validatorService.IsOnlyNumber(phone))
                    .WithMessage(localizer["{PropertyName} should be all letters."])
                .MustAsync(async (phone, _) => !await employeeService.ExistsWithPhoneNumberAsync(phone!))
                   .WithMessage((_, phone) => string.Format(localizer["Phone number {0} is already registered."], phone))
                .Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));

            RuleFor(x => x.Birthday).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."])
                .LessThan(DateTime.MaxValue).WithMessage(localizer["{PropertyName} should be less than max value"])
                .MustAsync(async (age, _) => await employeeService.GreaterThan18(age))
                    .WithMessage(localizer["{PropertyName} you must be of legal age to register."]);
        }
    }
}
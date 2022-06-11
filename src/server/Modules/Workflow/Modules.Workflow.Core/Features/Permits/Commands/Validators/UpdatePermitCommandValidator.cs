// --------------------------------------------------------------------------------------------------
// <copyright file="UpdatePermitCommandValidator.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Commands.Validators
{
    public class UpdatePermitCommandValidator : AbstractValidator<UpdatePermitCommand>
    {
        public UpdatePermitCommandValidator(
            IPermitService permitService,
            IStringLocalizer<UpdatePermitCommandValidator> localizer)
        {
            RuleFor(x => x.Id).NotNull()
               .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);

            RuleFor(x => x.EmployeeId).NotNull()
                .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);

            RuleFor(x => x.PermitTypeId).NotNull()
                .NotEqual(Guid.Empty).WithMessage(_ => localizer["{PropertyName} cannot be null or empty."]);

            RuleFor(x => x.StartDatePermit)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."]);

            RuleFor(x => x.EndDatePermit)
                .NotEmpty().WithMessage(localizer["{PropertyName} must not be empty."]);

            RuleFor(x => x)
                .MustAsync(async (x, _) => await permitService.IsOverlappingDatesAsync(x.EmployeeId, x.StartDatePermit, x.EndDatePermit))
                    .WithMessage(localizer["Should be no overlapping employee permissions."])
                    .DependentRules(() =>
                    {
                        RuleFor(x => x)
                         .MustAsync(async (x, _) => await permitService.IsPermitsGreaterThan30Days(x.StartDatePermit, x.EndDatePermit))
                            .WithMessage(localizer["Permits with a duration of more than 30 days are not allowed."]);
                    });
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="UpdatePermitCommand.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Wrapper;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Commands
{
    public class UpdatePermitCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid PermitTypeId { get; set; }

        public DateTime StartDatePermit { get; set; }

        public DateTime EndDatePermit { get; set; }
    }
}
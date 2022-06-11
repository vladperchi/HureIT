// --------------------------------------------------------------------------------------------------
// <copyright file="RemovePermitTypeCommand.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Wrapper;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands
{
    public class RemovePermitTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public RemovePermitTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
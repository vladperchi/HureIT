// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveEmployeeCommand.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Wrapper;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Commands
{
    public class RemoveEmployeeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveEmployeeCommand(Guid id)
        {
            Id = id;
        }
    }
}
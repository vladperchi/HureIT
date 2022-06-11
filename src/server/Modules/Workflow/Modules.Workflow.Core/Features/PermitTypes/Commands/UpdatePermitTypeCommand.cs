﻿// --------------------------------------------------------------------------------------------------
// <copyright file="UpdatePermitTypeCommand.cs" company="HureIT">
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
    public class UpdatePermitTypeCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CodeInternal { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeNotFoundException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Net;
using HureIT.Shared.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Exceptions
{
    public class EmployeeNotFoundException : CustomException
    {
        public Guid Id { get; }

        public EmployeeNotFoundException(IStringLocalizer localizer, Guid id)
            : base(localizer[$"Employee with Id: {id} was not found."], null, HttpStatusCode.NotFound)
        {
            Id = id;
        }
    }
}
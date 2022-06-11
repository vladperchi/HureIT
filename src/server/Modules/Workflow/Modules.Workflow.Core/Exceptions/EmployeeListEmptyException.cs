// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeListEmptyException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using HureIT.Shared.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Exceptions
{
    public class EmployeeListEmptyException : CustomException
    {
        public EmployeeListEmptyException(IStringLocalizer localizer)
            : base(localizer["Employee list is empty..."], null, HttpStatusCode.NoContent)
        {
        }
    }
}
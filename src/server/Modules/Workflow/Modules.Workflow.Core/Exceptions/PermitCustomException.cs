// --------------------------------------------------------------------------------------------------
// <copyright file="PermitCustomException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Localization;
using HureIT.Shared.Core.Exceptions;

namespace HureIT.Modules.Workflow.Core.Exceptions
{
    public class PermitCustomException : CustomException
    {
        public PermitCustomException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["Permit errors have occurred..."], errors, HttpStatusCode.InternalServerError)
        {
        }
    }
}
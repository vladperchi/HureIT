// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeCustomException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using HureIT.Shared.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Core.Exceptions
{
    public class PermitTypeCustomException : CustomException
    {
        public PermitTypeCustomException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["PermitType errors have occurred..."], errors, HttpStatusCode.InternalServerError)
        {
        }
    }
}
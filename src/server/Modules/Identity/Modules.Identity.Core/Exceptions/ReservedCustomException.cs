// --------------------------------------------------------------------------------------------------
// <copyright file="ReservedCustomException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using HureIT.Shared.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Core.Exceptions
{
    public class ReservedCustomException : CustomException
    {
        public ReservedCustomException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["Method reserved for in-scope initialization"], errors, HttpStatusCode.MethodNotAllowed)
        {
        }
    }
}
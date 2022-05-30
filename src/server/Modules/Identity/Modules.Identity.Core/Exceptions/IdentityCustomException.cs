// --------------------------------------------------------------------------------------------------
// <copyright file="IdentityCustomException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Localization;
using HureIT.Shared.Core.Exceptions;

namespace HureIT.Modules.Identity.Core.Exceptions
{
    public class IdentityCustomException : CustomException
    {
        public IdentityCustomException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["Identity errors have occurred..."], errors, HttpStatusCode.InternalServerError)
        {
        }
    }
}
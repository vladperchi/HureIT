// --------------------------------------------------------------------------------------------------
// <copyright file="ValidationCustomException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Localization;

namespace HureIT.Shared.Core.Exceptions
{
    public class ValidationCustomException : CustomException
    {
        public ValidationCustomException(IStringLocalizer localizer, List<string> errors)
            : base(localizer["Validation errors have occurred..."], errors, HttpStatusCode.BadRequest)
        {
        }
    }
}
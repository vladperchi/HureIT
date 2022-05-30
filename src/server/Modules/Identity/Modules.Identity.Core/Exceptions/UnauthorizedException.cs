// --------------------------------------------------------------------------------------------------
// <copyright file="UnauthorizedException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using HureIT.Shared.Core.Exceptions;

namespace HureIT.Modules.Identity.Core.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.Unauthorized)
            : base(message, errors, statusCode)
        {
        }
    }
}
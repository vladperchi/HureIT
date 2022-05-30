// --------------------------------------------------------------------------------------------------
// <copyright file="UserListEmptyException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using HureIT.Shared.Core.Exceptions;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Core.Exceptions
{
    public class UserListEmptyException : CustomException
    {
        public UserListEmptyException(IStringLocalizer localizer)
            : base(localizer["User list is empty..."], null, HttpStatusCode.NoContent)
        {
        }
    }
}
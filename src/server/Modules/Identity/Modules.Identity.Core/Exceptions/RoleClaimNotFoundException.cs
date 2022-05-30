// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimNotFoundException.cs" company="HureIT">
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
    public class RoleClaimNotFoundException : CustomException
    {
        public int Id { get; }

        public RoleClaimNotFoundException(IStringLocalizer localizer, int id)
            : base(localizer[$"Role Claim with Id: {id} was not found."], null, HttpStatusCode.NotFound)
        {
            Id = id;
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="RoleNotFoundException.cs" company="HureIT">
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
    public class RoleNotFoundException : CustomException
    {
        public string Id { get; }

        public RoleNotFoundException(IStringLocalizer localizer, string id)
            : base(localizer[$"Role with Id: {id} was not found."], null, HttpStatusCode.NotFound)
        {
            Id = id;
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="HavePermission.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;

namespace HureIT.Shared.Infrastructure.Permissions
{
    public class HavePermission : AuthorizeAttribute
    {
        public HavePermission(string permission)
        {
            Policy = permission;
        }
    }
}
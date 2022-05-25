// --------------------------------------------------------------------------------------------------
// <copyright file="UserRolesRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace HureIT.Shared.DTO.Identity.Users
{
    public class UserRolesRequest
    {
        public List<UserRoleModel> UserRoles { get; set; } = new();
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermissionResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace HureIT.Shared.DTO.Identity.Roles
{
    public class PermissionResponse
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public List<RoleClaimModel> RoleClaims { get; set; }
    }
}
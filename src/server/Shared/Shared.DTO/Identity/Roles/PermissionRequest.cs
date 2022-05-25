// --------------------------------------------------------------------------------------------------
// <copyright file="PermissionRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace HureIT.Shared.DTO.Identity.Roles
{
    public class PermissionRequest
    {
        public string RoleId { get; set; }

        public IList<RoleClaimModel> RoleClaims { get; set; }
    }
}
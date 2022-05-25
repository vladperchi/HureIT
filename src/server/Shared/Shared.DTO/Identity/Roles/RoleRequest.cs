// --------------------------------------------------------------------------------------------------
// <copyright file="RoleRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.DTO.Identity.Roles
{
    public class RoleRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
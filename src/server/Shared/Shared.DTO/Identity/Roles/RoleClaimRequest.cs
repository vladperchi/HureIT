// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.DTO.Identity.Roles
{
    public class RoleClaimRequest
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public string Group { get; set; }
    }
}
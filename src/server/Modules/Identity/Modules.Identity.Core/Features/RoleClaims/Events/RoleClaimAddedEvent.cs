// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimAddedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Identity.Core.Features.RoleClaims.Events
{
    public class RoleClaimAddedEvent : Event
    {
        public int Id { get; }

        public string RoleId { get; }

        public string ClaimType { get; }

        public string ClaimValue { get; }

        public string Group { get; }

        public string Description { get; }

        public RoleClaimAddedEvent(HureRoleClaim roleClaim)
        {
            Id = roleClaim.Id;
            RoleId = roleClaim.RoleId;
            Group = roleClaim.Group;
            ClaimType = roleClaim.ClaimType;
            ClaimValue = roleClaim.ClaimValue;
            Description = roleClaim.Description;
            AggregateId = Guid.TryParse(roleClaim.Id.ToString(), out var aggregateId) ? aggregateId : Guid.NewGuid();
            RelatedEntities = new[] { typeof(HureRoleClaim) };
            EventDescription = $"Added RoleClaim:{roleClaim.ClaimValue}:::RoleId:{roleClaim.RoleId}:::Id:{roleClaim.Id}";
        }
    }
}
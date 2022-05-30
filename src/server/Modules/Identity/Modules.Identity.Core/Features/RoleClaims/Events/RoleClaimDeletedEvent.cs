// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimDeletedEvent.cs" company="HureIT">
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
    public class RoleClaimDeletedEvent : Event
    {
        public int Id { get; }

        public RoleClaimDeletedEvent(int id)
{
            Id = id;
            AggregateId = Guid.TryParse(id.ToString(), out var aggregateId) ? aggregateId : Guid.NewGuid();
            RelatedEntities = new[] { typeof(HureRoleClaim) };
            EventDescription = $"Deleted RoleClaim:{id}";
        }
    }
}
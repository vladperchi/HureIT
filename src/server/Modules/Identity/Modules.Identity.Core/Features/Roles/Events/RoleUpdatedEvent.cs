// --------------------------------------------------------------------------------------------------
// <copyright file="RoleUpdatedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Identity.Core.Features.Roles.Events
{
    public class RoleUpdatedEvent : Event
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public RoleUpdatedEvent(HureRole role)
        {
            Id = role.Id;
            Name = role.Name;
            Description = role.Description;
            AggregateId = Guid.TryParse(role.Id, out var aggregateId)
                ? aggregateId
                : Guid.NewGuid();
            RelatedEntities = new[] { typeof(HureRole) };
            EventDescription = $"Updated Role:{role.Name}:::Id:{role.Id}";
        }
    }
}
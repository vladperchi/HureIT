﻿// --------------------------------------------------------------------------------------------------
// <copyright file="UserDeletedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Identity.Core.Features.Users.Events
{
    public class UserDeletedEvent : Event
    {
        public string Id { get; }

        public UserDeletedEvent(string id)
        {
            Id = id;
            AggregateId = Guid.TryParse(id, out var aggregateId) ? aggregateId : Guid.NewGuid();
            RelatedEntities = new[] { typeof(HureUser) };
            EventDescription = $"Deleted User:{id}";
        }
    }
}
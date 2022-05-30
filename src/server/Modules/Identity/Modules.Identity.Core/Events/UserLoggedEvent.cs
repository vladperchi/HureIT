// --------------------------------------------------------------------------------------------------
// <copyright file="UserLoggedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Identity.Core.Events
{
    public class UserLoggedEvent : Event
    {
        public Guid UserId { get; }

        public new DateTime Timestamp { get; }

        public UserLoggedEvent(Guid userId)
        {
            UserId = userId;
            Timestamp = DateTime.Now;
            AggregateId = userId;
            RelatedEntities = new[] { typeof(HureUser) };
            EventDescription = $"Logged User Id: {userId} Event.";
        }
    }
}
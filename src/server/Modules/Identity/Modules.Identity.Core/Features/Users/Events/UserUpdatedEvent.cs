// --------------------------------------------------------------------------------------------------
// <copyright file="UserUpdatedEvent.cs" company="HureIT">
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
    public class UserUpdatedEvent : Event
    {
        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string UserName { get; }

        public string PhoneNumber { get; }

        public UserUpdatedEvent(HureUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;
            AggregateId = Guid.TryParse(user.Id, out var aggregateId)
                ? aggregateId
                : Guid.NewGuid();
            RelatedEntities = new[] { typeof(HureUser) };
            EventDescription = $"Updated User:{user.UserName}:::Email:{user.Email}:::Id:{user.Id}";
        }
    }
}
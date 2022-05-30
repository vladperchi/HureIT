// --------------------------------------------------------------------------------------------------
// <copyright file="UserPictureUpdateEvent.cs" company="HureIT">
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
    public class UserPictureUpdateEvent : Event
    {
        public string Id { get; }

        public string ImageUrl { get; }

        public UserPictureUpdateEvent(HureUser user)
        {
            Id = user.Id;
            ImageUrl = user.ImageUrl;
            AggregateId = Guid.TryParse(user.Id, out var aggregateId)
                ? aggregateId
                : Guid.NewGuid();
            RelatedEntities = new[] { typeof(HureUser) };
            EventDescription = $"Updated User Picture:{user.ImageUrl}:::Id:{user.Id}";
        }
    }
}
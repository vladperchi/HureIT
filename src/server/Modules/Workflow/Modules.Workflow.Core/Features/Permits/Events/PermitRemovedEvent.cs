// --------------------------------------------------------------------------------------------------
// <copyright file="PermitRemovedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Features.Elevators.Events
{
    public class PermitRemovedEvent : Event
    {
        public Guid Id { get; }

        public PermitRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(Permit) };
            EventDescription = $"Deleted Permission:{id}";
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeRemovedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Events
{
    public class PermitTypeRemovedEvent : Event
    {
        public Guid Id { get; }

        public PermitTypeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(PermitType) };
            EventDescription = $"Deleted Permit Type:{id}";
        }
    }
}
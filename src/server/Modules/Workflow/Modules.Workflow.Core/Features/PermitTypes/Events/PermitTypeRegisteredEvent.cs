// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeRegisteredEvent.cs" company="HureIT">
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
    public class PermitTypeRegisteredEvent : Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }

        public PermitTypeRegisteredEvent(PermitType permitType)
        {
            Id = permitType.Id;
            Name = permitType.Name;
            Description = permitType.Description;
            IsEnabled = permitType.IsEnabled;
            AggregateId = permitType.Id;
            RelatedEntities = new[] { typeof(PermitType) };
            EventDescription = $"Registered Permit Type:{permitType.Name} ({permitType.CodeInternal}):::Id:{permitType.Id}";
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRemovedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Events
{
    public class EmployeeRemovedEvent : Event
    {
        public Guid Id { get; }

        public EmployeeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(Employee) };
            EventDescription = $"Deleted Employee:{id}";
        }
    }
}
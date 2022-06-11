// --------------------------------------------------------------------------------------------------
// <copyright file="PermitUpdatedEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Events
{
    public class PermitUpdatedEvent : Event
    {
        public Guid Id { get; }

        public Guid EmployeeId { get; set; }

        public Guid PermitTypeId { get; set; }

        public DateTime StartDatePermit { get; set; }

        public DateTime EndDatePermit { get; set; }

        public PermitUpdatedEvent(Permit permit)
        {
            Id = permit.Id;
            StartDatePermit = permit.StartDatePermit;
            EndDatePermit = permit.EndDatePermit;
            EmployeeId = permit.EmployeeId;
            PermitTypeId = permit.PermitTypeId;
            AggregateId = permit.Id;
            RelatedEntities = new[] { typeof(Permit) };
            EventDescription = $"Update Permission:{permit.PermitType.PermitName}:::Date:{permit.StartDatePermit} - {permit.EndDatePermit}:::Employee:{permit.Employee.FullName}:::Id:{permit.Id}";
        }
    }
}
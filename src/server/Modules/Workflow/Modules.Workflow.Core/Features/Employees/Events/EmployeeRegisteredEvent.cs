// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRegisteredEvent.cs" company="HureIT">
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
    public class EmployeeRegisteredEvent : Event
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public EmployeeRegisteredEvent(Employee employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            PhoneNumber = employee.PhoneNumber;
            Birthday = employee.Birthday;
            Gender = employee.Gender;
            Email = employee.Email;
            AdmissionDate = employee.AdmissionDate;
            ImageUrl = employee.ImageUrl;
            IsActive = employee.IsActive;
            AggregateId = employee.Id;
            RelatedEntities = new[] { typeof(Employee) };
            EventDescription = $"Registered Employee:{employee.FullName}:::Email:{employee.Email}:::Id:{employee.Id}";
}
    }
}
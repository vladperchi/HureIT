// --------------------------------------------------------------------------------------------------
// <copyright file="Employee.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public string DateOfBirth => $"{Birthday.ToString("G", CultureInfo.CurrentCulture)}";

        public string Admission => $"{AdmissionDate.ToString("G", CultureInfo.CurrentCulture)}";
    }
}
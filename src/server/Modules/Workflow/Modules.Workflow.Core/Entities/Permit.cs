// --------------------------------------------------------------------------------------------------
// <copyright file="Permit.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Entities
{
    public class Permit : BaseEntity
    {
        public Guid EmployeeId { get; set; }

        public Guid PermitTypeId { get; set; }

        public DateTime StartDatePermit { get; set; }

        public DateTime EndDatePermit { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual PermitType PermitType { get; set; }

        [NotMapped]
        public string StartDate => $"{StartDatePermit.ToString("G", CultureInfo.CurrentCulture)}";

        public string EndDate => $"{EndDatePermit.ToString("G", CultureInfo.CurrentCulture)}";
    }
}
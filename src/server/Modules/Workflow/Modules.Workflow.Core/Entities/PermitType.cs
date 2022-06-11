// --------------------------------------------------------------------------------------------------
// <copyright file="PermitType.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations.Schema;
using HureIT.Shared.Core.Domain;

namespace HureIT.Modules.Workflow.Core.Entities
{
    public class PermitType : BaseEntity
    {
        public string Name { get; set; }

        public string CodeInternal { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }

        [NotMapped]
        public string PermitName => $"{Name} ({CodeInternal.ToUpper()})";

        public string PermitDescription => Description;
    }
}
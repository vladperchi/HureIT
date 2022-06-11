// --------------------------------------------------------------------------------------------------
// <copyright file="PermitFilterSpecification.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace HureIT.Modules.Workflow.Core.Specifications
{
    public class PermitFilterSpecification : Specification<Permit>
    {
        public PermitFilterSpecification(string searchString)
        {
            Includes.Add(x => x.Employee);
            Includes.Add(x => x.PermitType);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = x => EF.Functions.Like(x.Employee.FullName.ToLower(), $"%{searchString.ToLower()}%")
                || EF.Functions.Like(x.PermitType.PermitName.ToLower(), $"%{searchString.ToLower()}%");
            }
            else
            {
                Criteria = _ => true;
            }
        }
    }
}
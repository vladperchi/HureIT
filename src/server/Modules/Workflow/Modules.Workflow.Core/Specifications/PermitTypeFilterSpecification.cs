// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeFilterSpecification.cs" company="HureIT">
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
    public class PermitTypeFilterSpecification : Specification<PermitType>
    {
        public PermitTypeFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = x => !string.IsNullOrWhiteSpace(x.CodeInternal)
                && (EF.Functions.Like(x.Name.ToLower(), $"%{searchString.ToLower()}%")
                || EF.Functions.Like(x.Description.ToLower(), $"%{searchString.ToLower()}%")
                || EF.Functions.Like(x.CodeInternal.ToUpper(), $"%{searchString.ToUpper()}%"));
            }
            else
            {
                Criteria = x => !string.IsNullOrWhiteSpace(x.CodeInternal);
            }
        }
    }
}
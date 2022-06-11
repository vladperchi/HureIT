// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeFilterSpecification.cs" company="HureIT">
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
    public class EmployeeFilterSpecification : Specification<Employee>
    {
        public EmployeeFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = x => !string.IsNullOrWhiteSpace(x.Email)
                && (EF.Functions.Like(x.FullName.ToLower(), $"%{searchString.ToLower()}%")
                || EF.Functions.Like(x.PhoneNumber.ToLower(), $"%{searchString.ToLower()}%")
                || EF.Functions.Like(x.Email.ToLower(), $"%{searchString.ToLower()}%"));
            }
            else
            {
                Criteria = x => !string.IsNullOrWhiteSpace(x.Email);
            }
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="GetEmployeesWithPermitsResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace HureIT.Shared.DTO.Workflow.Employees
{
    public record GetEmployeesWithPermitsResponse(Guid Id, string FullName, string PhoneNumber, string Email)
    {
        public int TotalPermits { get; set; }

        public IEnumerable<string> PermitList { get; set; }

    }
}
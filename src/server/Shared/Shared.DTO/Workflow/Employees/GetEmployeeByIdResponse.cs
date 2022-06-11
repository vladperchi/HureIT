// --------------------------------------------------------------------------------------------------
// <copyright file="GetEmployeeByIdResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace HureIT.Shared.DTO.Workflow.Employees
{
    public record GetEmployeeByIdResponse(Guid Id, string FullName, string PhoneNumber, string DateOfBirth, string Gender, string Email, string Admission, string ImageUrl, bool IsActive)
    {
        public int TotalPermits { get; set; }

        public IEnumerable<string> PermitList { get; set; }
    }
}
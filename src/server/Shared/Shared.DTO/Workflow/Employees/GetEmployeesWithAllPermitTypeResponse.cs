// --------------------------------------------------------------------------------------------------
// <copyright file="GetEmployeesWithAllPermitTypeResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.DTO.Workflow.Employees
{
    public class GetEmployeesWithAllPermitTypeResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Admission { get; set; }
    }
}
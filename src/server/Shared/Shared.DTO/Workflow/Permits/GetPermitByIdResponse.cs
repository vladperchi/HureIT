// --------------------------------------------------------------------------------------------------
// <copyright file="GetPermitByIdResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.DTO.Workflow.Permits
{
    // public record GetPermitByIdResponse(Guid Id, Guid EmployeeId, string EmployeeName, Guid PermitTypeId, string PermitTypeName);

    public class GetPermitByIdResponse
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid PermitTypeId { get; set; }

        public string EmployeeName { get; set; }

        public string PermitName { get; set; }

        public string PermitDescription { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="GetAllPermitsResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.DTO.Workflow.Permits
{
    public record GetAllPermitsResponse(Guid Id, Guid EmployeeId, Guid PermitTypeId, DateTime StartDatePermit, DateTime EndDatePermit, string EmployeeName, string PermitTypeName, string PermitDescription, string StartDate, string EndDate);
}
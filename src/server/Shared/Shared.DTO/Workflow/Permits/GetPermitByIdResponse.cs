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
    public record GetPermitByIdResponse(Guid Id, Guid EmployeeId, string EmployeeFullName, Guid PermissionTypeId, string PermissionTypeName);
}
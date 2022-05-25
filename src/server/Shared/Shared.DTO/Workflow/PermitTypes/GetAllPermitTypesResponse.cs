// --------------------------------------------------------------------------------------------------
// <copyright file="GetAllPermitTypesResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.DTO.Workflow.PermitTypes
{
    public record GetAllPermitTypesResponse(Guid Id, string Name, string Description, string CodeInternal, bool IsEnabled);
}
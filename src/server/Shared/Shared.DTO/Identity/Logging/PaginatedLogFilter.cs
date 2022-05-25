// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedLogFilter.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.DTO.Filters;

namespace HureIT.Shared.DTO.Identity.Logging
{
    public class PaginatedLogFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string MessageType { get; set; }
    }
}
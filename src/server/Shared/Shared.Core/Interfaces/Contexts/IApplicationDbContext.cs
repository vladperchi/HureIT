// --------------------------------------------------------------------------------------------------
// <copyright file="IApplicationDbContext.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using HureIT.Shared.Core.Logging;

namespace HureIT.Shared.Core.Interfaces.Contexts
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="IIdentityDbContext.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.Core.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HureIT.Modules.Identity.Core.Abstractions
{
    public interface IIdentityDbContext : IDbContext
    {
        public DbSet<HureUser> Users { get; set; }

        public DbSet<HureRole> Roles { get; set; }

        public DbSet<HureRoleClaim> RoleClaims { get; set; }
    }
}
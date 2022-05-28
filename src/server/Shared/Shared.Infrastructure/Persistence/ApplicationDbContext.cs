// --------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Core.Logging;
using HureIT.Shared.Core.Interfaces.Contexts;
using HureIT.Shared.Core.Settings;
using HureIT.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HureIT.Shared.Core.Constants;

namespace HureIT.Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;

        protected string Schema => SchemesConstant.Application;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<PersistenceSettings> persistenceOptions)
                : base(options)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyApplicationConfiguration(_persistenceOptions);
        }
    }
}
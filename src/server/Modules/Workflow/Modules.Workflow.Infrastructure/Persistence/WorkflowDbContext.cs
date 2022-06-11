// --------------------------------------------------------------------------------------------------
// <copyright file="WorkflowDbContext.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Logging;
using HureIT.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HureIT.Shared.Core.Interfaces.Serialization.Serializer;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Settings;
using Microsoft.Extensions.Options;
using HureIT.Shared.Core.Interfaces.Services.Identity;

namespace HureIT.Modules.Workflow.Infrastructure.Persistence
{
    public sealed class WorkflowDbContext : ModuleDbContext, IWorkflowDbContext
    {
        protected override string Schema => SchemesConstant.Workflow;

        public WorkflowDbContext(
            DbContextOptions<WorkflowDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json,
            ICurrentUser currentUser)
                : base(options, mediator, eventLogger, persistenceOptions, json, currentUser)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<PermitType> PermitTypes { get; set; }

        public DbSet<Permit> Permits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="IWorkflowDbContext.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HureIT.Modules.Workflow.Core.Abstractions
{
    public interface IWorkflowDbContext : IDbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<PermitType> PermitTypes { get; set; }

        public DbSet<Permit> Permits { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Infrastructure.Persistence;
using HureIT.Modules.Workflow.Infrastructure.Services;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HureIT.Modules.Workflow.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorkflowInfrastructure(this IServiceCollection services)
        {
            services.AddDatabaseContext<WorkflowDbContext>();
            services.AddScoped<IWorkflowDbContext>(provider => provider.GetService<WorkflowDbContext>());
            services.AddTransient<IDbSeeder, WorkflowDbSeeder>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IPermitTypeService, PermitTypeService>();
            services.AddTransient<IPermitService, PermitService>();
            Log.Logger.Information("Connected Scheme Workflow and its tables Successfully");
            return services;
        }

        public static IServiceCollection AddWorkflowValidation(this IServiceCollection services)
        {
            services.AddControllers().AddWorkflowValidation();
            return services;
        }
    }
}
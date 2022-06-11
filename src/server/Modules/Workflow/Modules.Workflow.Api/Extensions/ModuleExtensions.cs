// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Workflow.Core.Extensions;
using HureIT.Modules.Workflow.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HureIT.Modules.Workflow.Api.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddWorkflowModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddWorkflowCore()
                .AddWorkflowInfrastructure()
                .AddWorkflowValidation();
            return services;
        }

        public static IApplicationBuilder UseWorkflowModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
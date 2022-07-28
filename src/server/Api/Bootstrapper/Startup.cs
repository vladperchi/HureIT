// --------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Identity.Api.Extensions;
using HureIT.Modules.Workflow.Api.Extensions;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HureIT.Bootstrapper
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                  .AddDistributedMemoryCache()
                  .AddSerialization(_config)
                  .AddSharedInfrastructure(_config)
                  .AddSharedApplication(_config)
                  .AddIdentityModule(_config)
                  .AddWorkflowModule(_config);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure(_config);
        }
    }
}
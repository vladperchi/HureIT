// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Modules.Identity.Core.Extensions;
using HureIT.Modules.Identity.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HureIT.Modules.Identity.Api.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityCore()
                .AddIdentityInfrastructure(configuration);
            return services;
        }

        public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
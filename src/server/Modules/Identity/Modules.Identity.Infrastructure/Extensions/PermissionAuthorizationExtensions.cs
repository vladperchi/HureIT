// --------------------------------------------------------------------------------------------------
// <copyright file="PermissionAuthorizationExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Infrastructure.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HureIT.Modules.Identity.Infrastructure.Extensions
{
    public static class PermissionAuthorizationExtensions
    {
        internal static IServiceCollection AddPermissions(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            return services;
        }
    }
}
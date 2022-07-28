// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using HureIT.Modules.Identity.Core.Abstractions;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Modules.Identity.Core.Settings;
using HureIT.Modules.Identity.Infrastructure.Persistence;
using HureIT.Modules.Identity.Infrastructure.Services;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Interfaces.Services.Dashboard;
using HureIT.Shared.Core.Interfaces.Services.Identity;
using HureIT.Shared.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HureIT.Modules.Identity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services
                .AddHttpContextAccessor()
                .AddScoped<ICurrentUser, CurrentUser>()
                .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>())
                .Configure<JwtSettings>(configuration.GetSection("JwtSettings"))
                .AddTransient<ITokenService, TokenService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IRoleClaimService, RoleClaimService>()
                .AddTransient<IStatsService, StatsService>()
                .AddDatabaseContext<IdentityDbContext>()
                .AddScoped<IIdentityDbContext>(provider => provider.GetService<IdentityDbContext>());
            Log.Logger.Information("Established Scheme Identity and its tables Successfully");
            services
                .AddIdentity<HureUser, HureRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IDbSeeder, IdentityDbSeeder>();
            services.AddPermissions(configuration);
            Log.Logger.Information("Added Access Permissions Successfully");
            services.AddJwtAuthentication(configuration);
            Log.Logger.Information("Configured Jwt Authentication Successfully");
            return services;
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HureIT.Shared.Infrastructure.Extensions
{
    internal static class MiddlewareExtensions
    {
        internal static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app) => app.UseMiddleware<ExceptionHandlerMiddleware>();

        internal static IServiceCollection AddExceptionMiddleware(this IServiceCollection services) => services.AddScoped<ExceptionHandlerMiddleware>();
    }
}
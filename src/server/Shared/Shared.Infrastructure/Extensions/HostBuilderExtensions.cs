// --------------------------------------------------------------------------------------------------
// <copyright file="HostBuilderExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace HureIT.Shared.Infrastructure.Extensions
{
     internal static class HostBuilderExtensions
    {
        internal static IHostBuilder UseSerilog(this IHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            SerilogHostBuilderExtensions.UseSerilog(builder);
            return builder;
        }
    }
}
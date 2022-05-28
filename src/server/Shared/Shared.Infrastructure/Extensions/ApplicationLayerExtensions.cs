// --------------------------------------------------------------------------------------------------
// <copyright file="ApplicationLayerExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Settings;
using HureIT.Shared.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace HureIT.Shared.Infrastructure.Extensions
{
    public static class ApplicationLayerExtensions
    {
        internal static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ILoggerService, LoggerService>();
            services.AddScoped<IJobService, HangfireService>();
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IMailService, SmtpMailService>();
            services.AddTransient<ITemplateMailService, TemplateMailService>();
            services.AddTransient<IExcelService, ExcelService>();
            services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));
            services.Configure<TemplateMailSettings>(config.GetSection(nameof(TemplateMailSettings)));
            services.Configure<CorsSettings>(config.GetSection(nameof(CorsSettings)));
            services.Configure<SwaggerSettings>(config.GetSection(nameof(SwaggerSettings)));

            return services;
        }
    }
}
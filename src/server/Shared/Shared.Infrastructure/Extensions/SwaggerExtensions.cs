﻿// --------------------------------------------------------------------------------------------------
// <copyright file="SwaggerExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Core.Settings;
using HureIT.Shared.Infrastructure.Swagger.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HureIT.Shared.Infrastructure.Extensions
{
    public static class SwaggerExtensions
    {
        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            if (config.GetValue<bool>("SwaggerSettings:Enable"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DefaultModelsExpandDepth(-1);
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = "swagger";
                    options.DisplayRequestDuration();
                    options.DocExpansion(DocExpansion.None);
                });
            }

            return app;
        }

        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            var settings = GetSwaggerSettings(services);
            if (settings?.Enable is true)
            {
                services.AddSwaggerGen(options =>
                {
                    options.AddSwaggerDocs(services);
                    options.OperationFilter<RemoveVersionParameterFilter>();
                    options.DocumentFilter<ReplaceVersionValueInPathFilter>();
                    options.DocInclusionPredicate((version, desc) =>
                    {
                        if (!desc.TryGetMethodInfo(out var methodInfo))
                        {
                            return false;
                        }

                        var versions = methodInfo
                            .DeclaringType?
                            .GetCustomAttributes(true)
                            .OfType<ApiVersionAttribute>()
                            .SelectMany(attr => attr.Versions);

                        var maps = methodInfo
                            .GetCustomAttributes(true)
                            .OfType<MapToApiVersionAttribute>()
                            .SelectMany(attr => attr.Versions)
                            .ToList();

                        return versions?.Any(v => $"v{v}" == version) == true
                               && (maps.Count == 0 || maps.Any(v => $"v{v}" == version));
                    });

                    var currentDomain = AppDomain.CurrentDomain;
                    foreach (var assembly in currentDomain.GetAssemblies())
                    {
                        if (!assembly.IsDynamic)
                        {
                            string xmlFile = $"{assembly.GetName().Name}.xml";
                            string xmlPath = Path.Combine(currentDomain.BaseDirectory, xmlFile);
                            if (File.Exists(xmlPath))
                            {
                                options.IncludeXmlComments(xmlPath);
                            }
                        }
                    }

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = StringKeys.SecuritySchemeName,
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = StringKeys.SecuritySchemeFormat,
                        Description = string.Format("Input your Bearer token in this format - \"Bearer {0}\" to access this {1} API", StringKeys.Token.ToLower(), StringKeys.AppName)
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                                Scheme = "Bearer",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            }, new List<string>()
                        },
                    });

                    options.MapType<TimeSpan>(() => new OpenApiSchema
                    {
                        Type = StringKeys.SchemaType,
                        Nullable = true,
                        Pattern = StringKeys.SchemaRegexPattern,
                        Example = new OpenApiString(StringKeys.SchemaTimeSpan)
                    });
                    options.EnableAnnotations();
                    options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                });
            }

            return services;
        }

        private static void AddSwaggerDocs(this SwaggerGenOptions options, IServiceCollection services)
        {
            var settings = GetSwaggerSettings(services);
            options.SwaggerDoc(settings.Version, new OpenApiInfo
            {
                Version = settings.Version,
                Title = settings.Title,
                Description = settings.Description,
                Contact = new OpenApiContact
                {
                    Name = settings.ContactName,
                    Email = settings.ContactEmail,
                    Url = new Uri(settings.ContactUrl),
                },
                License = new OpenApiLicense
                {
                    Name = settings.LicenseName,
                    Url = new Uri(settings.LicenseUrl),
                },
                TermsOfService = new Uri(settings.TermsUrl)
            });
        }

        private static SwaggerSettings GetSwaggerSettings(IServiceCollection services) =>
            services.GetOptions<SwaggerSettings>(nameof(SwaggerSettings));
    }
}
﻿// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using FluentValidation;
using HureIT.Shared.Core.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HureIT.Modules.Identity.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddPaginatedFilterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
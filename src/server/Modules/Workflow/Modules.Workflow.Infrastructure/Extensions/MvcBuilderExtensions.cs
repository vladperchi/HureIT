// --------------------------------------------------------------------------------------------------
// <copyright file="MvcBuilderExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using HureIT.Modules.Workflow.Core.Abstractions;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace HureIT.Modules.Workflow.Infrastructure.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddWorkflowValidation(this IMvcBuilder builder)
        {
            return builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(IWorkflowDbContext))));
        }
    }
}
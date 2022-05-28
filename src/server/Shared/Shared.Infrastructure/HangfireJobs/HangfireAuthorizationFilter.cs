// --------------------------------------------------------------------------------------------------
// <copyright file="HangfireAuthorizationFilter.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Hangfire.Dashboard;
using HureIT.Shared.Core.Constants;

namespace HureIT.Shared.Infrastructure.HangfireJobs
{
    /// <inheritdoc cref = "IDashboardAuthorizationFilter" />
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
           var currentUser = context.GetHttpContext().User;
           return currentUser.Identity.IsAuthenticated && currentUser.IsInRole(PermissionsConstant.Hangfire.View);
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermissionAuthorizationHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using HureIT.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;

namespace HureIT.Shared.Infrastructure.Permissions
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null)
            {
                await Task.CompletedTask;
            }

            var permissions = context.User.Claims.Where(x => x.Type == ClaimConstant.Permission &&
                                                             x.Value == requirement.Permission &&
                                                             x.Issuer == StringKeys.ClaimsIssuer);
            if (permissions.Any())
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
            }
        }
    }
}
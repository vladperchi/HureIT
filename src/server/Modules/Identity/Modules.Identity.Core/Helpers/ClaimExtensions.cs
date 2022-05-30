﻿// --------------------------------------------------------------------------------------------------
// <copyright file="ClaimExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.DTO.Identity.Roles;

using Microsoft.AspNetCore.Identity;

namespace HureIT.Modules.Identity.Core.Helpers
{
    public static class ClaimsHelper
    {
        public static void GetAllPermissions(this List<RoleClaimModel> allPermissions)
        {
            foreach (var module in typeof(PermissionsConstant).GetNestedTypes())
            {
                string moduleName = string.Empty;
                string moduleDescription = string.Empty;

                if (module.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() is DisplayNameAttribute displayNameAttribute)
                {
                    moduleName = displayNameAttribute.DisplayName;
                }

                if (module.GetCustomAttributes(typeof(DescriptionAttribute), true)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                {
                    moduleDescription = descriptionAttribute.Description;
                }

                foreach (var fi in module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                {
                    object propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                    {
                        allPermissions.Add(new() { Value = propertyValue.ToString(), Type = ClaimConstant.Permission, Group = moduleName, Description = moduleDescription });
                    }
                }
            }
        }

        public static async Task<IdentityResult> AddPermissionClaimAsync(this RoleManager<HureRole> roleManager, HureRole role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ClaimConstant.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new(ClaimConstant.Permission, permission));
            }

            return IdentityResult.Failed();
        }
    }
}
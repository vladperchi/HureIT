// --------------------------------------------------------------------------------------------------
// <copyright file="IdentityDbSeeder.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HureIT.Modules.Identity.Core.Abstractions;
using HureIT.Modules.Identity.Core.Constants;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Modules.Identity.Core.Helpers;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Identity.Infrastructure.Persistence
{
    internal class IdentityDbSeeder : IDbSeeder
    {
        private readonly ILogger<IdentityDbSeeder> _logger;
        private readonly IIdentityDbContext _context;
        private readonly UserManager<HureUser> _userManager;
        private readonly IStringLocalizer<IdentityDbSeeder> _localizer;
        private readonly RoleManager<HureRole> _roleManager;

        public IdentityDbSeeder(
            ILogger<IdentityDbSeeder> logger,
            IIdentityDbContext context,
            RoleManager<HureRole> roleManager,
            UserManager<HureUser> userManager,
            IStringLocalizer<IdentityDbSeeder> localizer)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _localizer = localizer;
        }

        public void Initialize()
        {
            try
            {
                AddDefaultRoles();
                AddAdministrator();
                AddOperator();
                _context.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError(_localizer["An error occurred while seeding data module Identity."]);
            }
        }

        private void AddDefaultRoles()
        {
            Task.Run(async () =>
            {
                var roleList = new List<string> { RolesConstant.Administrator, RolesConstant.Operator };
                foreach (string roleName in roleList)
                {
                    var role = new HureRole(roleName);
                    var roleInDb = await _roleManager.FindByNameAsync(roleName);
                    if (roleInDb == null)
                    {
                        role.Description = $"ROLE {roleName.ToUpper()}";
                        await _roleManager.CreateAsync(role);
                        _logger.LogInformation(string.Format(_localizer["Added '{0}' to Roles"], roleName));
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                var administratorRole = new HureRole(RolesConstant.Administrator, _localizer["Administrator role with default permissions"]);
                var administratorRoleInDb = await _roleManager.FindByNameAsync(RolesConstant.Administrator);
                if (administratorRoleInDb == null)
                {
                    await _roleManager.CreateAsync(administratorRole);
                    administratorRoleInDb = await _roleManager.FindByNameAsync(RolesConstant.Administrator);
                    _logger.LogInformation(_localizer["Seeded Administrator Role."]);
                }

                var superUser = new HureUser
                {
                    FirstName = "Vladperchi",
                    LastName = "Won",
                    Email = "vladperchi@hureit.com",
                    UserName = "vladperchi.won",
                    PhoneNumber = "00573043600512",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ImageUrl = StringKeys.UserImageGhost
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstant.AdministratorPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RolesConstant.Administrator);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(_localizer["Seeded default user with Administrator Role."]);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }

                foreach (string permission in typeof(PermissionsConstant).GetNestedStringValues())
                {
                    await _roleManager.AddPermissionClaimAsync(administratorRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddOperator()
        {
            Task.Run(async () =>
            {
                var operatorRole = new HureRole(RolesConstant.Operator, _localizer["Operator role with default permissions"]);
                var operatorRoleInDb = await _roleManager.FindByNameAsync(RolesConstant.Operator);
                if (operatorRoleInDb == null)
                {
                    await _roleManager.CreateAsync(operatorRole);
                    operatorRoleInDb = await _roleManager.FindByNameAsync(RolesConstant.Operator);
                    _logger.LogInformation(_localizer["Seeded Operator Role."]);
                }

                var operatorUser = new HureUser
                {
                    FirstName = "Paula Andrea",
                    LastName = "Won",
                    Email = "paulaandrea@hureit.com",
                    UserName = "paula.andrea",
                    PhoneNumber = "00573043600513",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ImageUrl = StringKeys.UserImageGhost
                };
                var operatorUserInDb = await _userManager.FindByEmailAsync(operatorUser.Email);
                if (operatorUserInDb == null)
                {
                    await _userManager.CreateAsync(operatorUser, UserConstant.StaffPassword);
                    var result = await _userManager.AddToRoleAsync(operatorUser, RolesConstant.Operator);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(_localizer["Seeded default user with Operator Role."]);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }
    }
}
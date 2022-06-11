// --------------------------------------------------------------------------------------------------
// <copyright file="RoleService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Modules.Identity.Core.Abstractions;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Modules.Identity.Core.Exceptions;
using HureIT.Modules.Identity.Core.Features.Roles.Events;
using HureIT.Modules.Identity.Infrastructure.Extensions;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Identity.Roles;
using HureIT.Shared.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Identity.Infrastructure.Services
{
    internal class RoleService : IRoleService
    {
        private readonly RoleManager<HureRole> _roleManager;
        private readonly UserManager<HureUser> _userManager;
        private readonly IIdentityDbContext _context;
        private readonly IStringLocalizer<RoleService> _localizer;
        private readonly IMapper _mapper;

        public RoleService(
            RoleManager<HureRole> roleManager,
            UserManager<HureUser> userManager,
            IMapper mapper,
            IIdentityDbContext context,
            IStringLocalizer<RoleService> localizer)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _localizer = localizer;
        }

        public async Task<Result<List<RoleResponse>>> GetAllAsync()
        {
            var data = await _roleManager.Roles.ToListAsync();
            _ = data ?? throw new RolListEmptyException(_localizer);
            var result = _mapper.Map<List<RoleResponse>>(data);
            return await Result<List<RoleResponse>>.SuccessAsync(result);
        }

        public async Task<Result<RoleResponse>> GetByIdAsync(string id)
        {
            var data = await _roleManager.Roles.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            _ = data ?? throw new RoleNotFoundException(_localizer, id);
            var result = _mapper.Map<RoleResponse>(data);
            return await Result<RoleResponse>.SuccessAsync(result);
        }

        public async Task<Result<string>> AddOrUpdateAsync(RoleRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var existingRole = await _roleManager.FindByNameAsync(request.Name);
                if (existingRole != null)
                {
                    throw new RoleAlreadyExistsException(_localizer);
                }

                var newRole = new HureRole(request.Name, request.Description);
                newRole.AddDomainEvent(new RoleAddedEvent(newRole));
                var result = await _roleManager.CreateAsync(newRole);
                await _context.SaveChangesAsync();
                if (result.Succeeded)
                {
                    return await Result<string>.SuccessAsync(newRole.Id, string.Format(_localizer["Role {0} Added."], request.Name));
                }
                else
                {
                    return await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                    throw new IdentityException(_localizer["An error occurred while added Rol"], result.GetErrorMessages(_localizer));
                }
            }
            else
            {
                var existingRole = await _roleManager.FindByIdAsync(request.Id);
                _ = existingRole ?? throw new RoleNotFoundException(_localizer, request.Id);
                if (DefaultRoles().Contains(existingRole.Name))
                {
                    return await Result<string>.SuccessAsync(string.Format(_localizer["Not allowed to modify {0} Role."], existingRole.Name));
                }

                existingRole.Name = request.Name;
                existingRole.NormalizedName = request.Name.ToUpper();
                existingRole.Description = request.Description;
                existingRole.AddDomainEvent(new RoleUpdatedEvent(existingRole));
                var result = await _roleManager.UpdateAsync(existingRole);
                if (result.Succeeded)
{
                    return await Result<string>.SuccessAsync(existingRole.Id, string.Format(_localizer["Role {0} Updated."], existingRole.Name));
                }
                else
                {
                    await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                    throw new IdentityException(_localizer["An error occurred while updated Rol"], result.GetErrorMessages(_localizer));
                }
            }
        }

        public async Task<Result<string>> DeleteAsync(string id)
        {
            var existingRole = await _roleManager.FindByIdAsync(id);
            _ = existingRole ?? throw new RoleNotFoundException(_localizer, id);
            if (DefaultRoles().Contains(existingRole.Name))
            {
                return await Result<string>.FailAsync(string.Format(_localizer["Not allowed to delete {0} Role."], existingRole.Name));
            }

            bool roleIsNotUsed = true;
            var allUsers = await _userManager.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                if (await _userManager.IsInRoleAsync(user, existingRole.Name))
                {
                    roleIsNotUsed = false;
                }
            }

            if (roleIsNotUsed)
            {
                existingRole.AddDomainEvent(new RoleDeletedEvent(id));
                var result = await _roleManager.DeleteAsync(existingRole);
                if (result.Succeeded)
                {
                    return await Result<string>.SuccessAsync(existingRole.Id, string.Format(_localizer["Role {0} Deleted."], existingRole.Name));
                }
                else
                {
                    return await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                    throw new IdentityException(_localizer["An error occurred while deleted Rol"], result.GetErrorMessages(_localizer));
                }
            }
            else
            {
                return await Result<string>.FailAsync(string.Format(_localizer["Not allowed to delete {0} Role as it is being used."], existingRole.Name));
            }
        }

        private static List<string> DefaultRoles() => typeof(RolesConstant).GetAllConstantValues<string>();

        public async Task<int> GetCountAsync() => await _roleManager.Roles.CountAsync();
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypesController.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries.Export;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Features.Filters;
using HureIT.Shared.DTO.Workflow.PermitTypes;
using HureIT.Shared.Infrastructure.Permissions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HureIT.Modules.Workflow.Api.Controllers
{
    [ApiVersion("1")]
    internal sealed class PermitTypesController : BaseController
    {
        [HttpGet]
        [HavePermission(PermissionsConstant.PermitTypes.ViewAll)]
        [SwaggerOperation(
            Summary = "Get Permit Type List.",
            Description = "List all permit types in the database. This can only be done by the registered user",
            OperationId = "GetAllAsync")]
        [SwaggerResponse(200, "Return permit type list.")]
        [SwaggerResponse(204, "Permit Type list not content.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedPermitTypeFilter filter)
        {
            var request = Mapper.Map<GetAllPermitTypesQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [HavePermission(PermissionsConstant.PermitTypes.View)]
        [SwaggerOperation(
            Summary = "Get Permit Type By Id.",
            Description = "We get the detail permit type by Id. This can only be done by the registered user",
            OperationId = "GetByIdAsync")]
        [SwaggerResponse(200, "Return permit type by Id.")]
        [SwaggerResponse(404, "Permit Type was not found.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, PermitType> filter)
        {
            var request = Mapper.Map<GetPermitTypeByIdQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [HavePermission(PermissionsConstant.PermitTypes.Create)]
        [SwaggerOperation(
            Summary = "Created Permit Type.",
            Description = "Created a permit type with all its values set. This can only be done by the registered user",
            OperationId = "CreateAsync")]
        [SwaggerResponse(201, "Return added permit type.")]
        [SwaggerResponse(400, "Permit Type already exists.")]
        [SwaggerResponse(500, "Permit Type Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> CreateAsync(CreatePermitTypeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [HavePermission(PermissionsConstant.PermitTypes.Update)]
        [SwaggerOperation(
            Summary = "Update Permit Type.",
            Description = "We get the permit type with its modified values. This can only be done by the registered user",
            OperationId = "UpdateAsync")]
        [SwaggerResponse(200, "Return updated permit type.")]
        [SwaggerResponse(404, "Permit Type was not found.")]
        [SwaggerResponse(500, "Permit Type Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> UpdateAsync(UpdatePermitTypeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [HavePermission(PermissionsConstant.PermitTypes.Remove)]
        [SwaggerOperation(
            Summary = "Remove Permit Type.",
            Description = "We get the removed permit type by Id. This can only be done by the registered user",
            OperationId = "RemoveAsync")]
        [SwaggerResponse(200, "Return removed permit type.")]
        [SwaggerResponse(404, "Permit Image was not found.")]
        [SwaggerResponse(405, "Not allowed to deletion.")]
        [SwaggerResponse(500, "Permit Image Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var response = await Mediator.Send(new RemovePermitTypeCommand(id));
            return Ok(response);
        }

        [HttpGet("export")]
        [HavePermission(PermissionsConstant.PermitTypes.Export)]
        [SwaggerOperation(
            Summary = "Export Permit Types To Excel.",
            Description = "We get an exported excel file permit type list. This can only be done by the registered user",
            OperationId = "ExportAsync")]
        [SwaggerResponse(200, "Return export permit types to excel.")]
        [SwaggerResponse(404, "Permit Types was not found.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> ExportAsync(string searchString = "") =>
            Ok(await Mediator.Send(new ExportPermitTypesQuery(searchString)));
    }
}
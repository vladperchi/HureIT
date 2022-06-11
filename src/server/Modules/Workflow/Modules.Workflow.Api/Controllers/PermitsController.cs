// --------------------------------------------------------------------------------------------------
// <copyright file="PermitsController.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Features.Permits.Commands;
using HureIT.Modules.Workflow.Core.Features.Permits.Queries;
using HureIT.Modules.Workflow.Core.Features.Permits.Queries.Export;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Features.Filters;
using HureIT.Shared.DTO.Workflow.Permits;
using HureIT.Shared.Infrastructure.Permissions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HureIT.Modules.Workflow.Api.Controllers
{
    [ApiVersion("1")]
    internal sealed class PermitsController : BaseController
    {
        [HttpGet]
        [HavePermission(PermissionsConstant.WorkPermits.ViewAll)]
        [SwaggerOperation(
            Summary = "Get Permit List.",
            Description = "List all permit in the database. This can only be done by the registered user",
            OperationId = "GetAllAsync")]
        [SwaggerResponse(200, "Return permit list.")]
        [SwaggerResponse(204, "Permit list not content.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedPermitFilter filter)
        {
            var request = Mapper.Map<GetAllPermitsQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [HavePermission(PermissionsConstant.WorkPermits.View)]
        [SwaggerOperation(
            Summary = "Get Permit By Id.",
            Description = "We get the detail permir by Id. This can only be done by the registered user",
            OperationId = "GetByIdAsync")]
        [SwaggerResponse(200, "Return permit by id.")]
        [SwaggerResponse(404, "Permit was not found.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Permit> filter)
        {
            var request = Mapper.Map<GetPermitByIdQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [HavePermission(PermissionsConstant.WorkPermits.Assign)]
        [SwaggerOperation(
            Summary = "Assign Permission.",
            Description = "Created a permit with all its values set. This can only be done by the created user",
            OperationId = "AssignAsync")]
        [SwaggerResponse(201, "Return created permit.")]
        [SwaggerResponse(400, "Permit already exists.")]
        [SwaggerResponse(500, "Permit Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> AssignAsync(AssignPermitCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [HavePermission(PermissionsConstant.WorkPermits.Update)]
        [SwaggerOperation(
            Summary = "Update Permit.",
            Description = "We get the permit with its modified values. This can only be done by the registered user",
            OperationId = "UpdateAsync")]
        [SwaggerResponse(200, "Return updated permit.")]
        [SwaggerResponse(404, "Permit was not found.")]
        [SwaggerResponse(500, "Permit Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> UpdateAsync(UpdatePermitCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [HavePermission(PermissionsConstant.WorkPermits.Remove)]
        [SwaggerOperation(
            Summary = "Remove Permit.",
            Description = "We get the removed permit by Id. This can only be done by the registered user",
            OperationId = "RemoveAsync")]
        [SwaggerResponse(200, "Return removed permit.")]
        [SwaggerResponse(404, "Permit was not found.")]
        [SwaggerResponse(500, "Permit Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var response = await Mediator.Send(new RemovePermitCommand(id));
            return Ok(response);
        }

        [HttpGet("export")]
        [HavePermission(PermissionsConstant.WorkPermits.Export)]
        [SwaggerOperation(
            Summary = "Export Permits To Excel.",
            Description = "We get an exported excel file permit list. This can only be done by the registered user",
            OperationId = "ExportAsync")]
        [SwaggerResponse(200, "Return export permits to excel.")]
        [SwaggerResponse(404, "Permits was not found.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> ExportAsync(string searchString = "") =>
            Ok(await Mediator.Send(new ExportPermitsQuery(searchString)));
    }
}
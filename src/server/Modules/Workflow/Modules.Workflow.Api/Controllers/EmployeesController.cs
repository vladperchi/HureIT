// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeesController.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Features.Employees.Commands;
using HureIT.Modules.Workflow.Core.Features.Employees.Queries;
using HureIT.Modules.Workflow.Core.Features.Employees.Queries.Export;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Features.Filters;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.DTO.Workflow.Employees;
using HureIT.Shared.Infrastructure.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HureIT.Modules.Workflow.Api.Controllers
{
    [ApiVersion("1")]
    internal sealed class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [HavePermission(PermissionsConstant.Employees.ViewAll)]
        [SwaggerOperation(
            Summary = "Get Employee List.",
            Description = "List all employee in the database. This can only be done by the registered user",
            OperationId = "GetAllAsync")]
        [SwaggerResponse(200, "Return employee list.")]
        [SwaggerResponse(204, "Employee list not content.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedEmployeeFilter filter)
        {
            var request = Mapper.Map<GetAllEmployeesQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [HavePermission(PermissionsConstant.Employees.View)]
        [SwaggerOperation(
            Summary = "Get Employee By Id.",
            Description = "We get the detail employee by Id. This can only be done by the registered user",
            OperationId = "GetByIdAsync")]
        [SwaggerResponse(200, "Return employee by id.")]
        [SwaggerResponse(404, "Employee was not found.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Employee> filter)
        {
            var request = Mapper.Map<GetEmployeeByIdQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("number-permits")]
        [HavePermission(PermissionsConstant.Employees.ViewList)]
        [SwaggerOperation(
            Summary = "Get Employee List Number Permits.",
            Description = "List of employees with the number of permits assigned in the database. This can only be done by the registered user",
            OperationId = "GetListWithPermitsAsync")]
        [SwaggerResponse(200, "Return employee list number permits.")]
        [SwaggerResponse(204, "Employee list not content.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetListWithPermitsAsync([FromQuery] PaginatedEmployeeFilter filter)
        {
            var request = Mapper.Map<GetEmployeesWithPermitsQuery>(filter);
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("picture/{id:guid}")]
        [HavePermission(PermissionsConstant.Employees.View)]
        [ResponseCache(NoStore = false, Location = ResponseCacheLocation.Client, Duration = 60)]
        [SwaggerOperation(
            Summary = "Get Picture Employee.",
            Description = "We get the employee associated to the building. This can only be done by the registered user",
            OperationId = "GetPictureAsync")]
        [SwaggerResponse(200, "Return picture employee by id.")]
        [SwaggerResponse(404, "employee was not found.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetPictureAsync(Guid id)
        {
            var response = await _employeeService.GetPictureAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [HavePermission(PermissionsConstant.Employees.Register)]
        [SwaggerOperation(
            Summary = "Registered Employee.",
            Description = "Registed a employee with all its values set. This can only be done by the registered user",
            OperationId = "RegisterAsync")]
        [SwaggerResponse(201, "Return registered employee.")]
        [SwaggerResponse(400, "Employee already exists.")]
        [SwaggerResponse(500, "Employee Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> RegisterAsync(RegisterEmployeeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [HavePermission(PermissionsConstant.Employees.Update)]
        [SwaggerOperation(
            Summary = "Update Employee.",
            Description = "We get the employee with its modified values. This can only be done by the registered user",
            OperationId = "UpdateAsync")]
        [SwaggerResponse(200, "Return updated employee.")]
        [SwaggerResponse(404, "Employee was not found.")]
        [SwaggerResponse(500, "Employee Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> UpdateAsync(UpdateEmployeeCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [HavePermission(PermissionsConstant.Employees.Remove)]
        [SwaggerOperation(
            Summary = "Remove Employee.",
            Description = "We get the removed employee by Id. This can only be done by the registered user",
            OperationId = "RemoveAsync")]
        [SwaggerResponse(200, "Return removed employee.")]
        [SwaggerResponse(404, "Employee was not found.")]
        [SwaggerResponse(500, "Employee Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var response = await Mediator.Send(new RemoveEmployeeCommand(id));
            return Ok(response);
        }

        [HttpGet("export")]
        [HavePermission(PermissionsConstant.Employees.Export)]
        [SwaggerOperation(
            Summary = "Export Employee List To Excel.",
            Description = "We get an exported excel file of all employees. This can only be done by the registered user",
            OperationId = "ExportAsync")]
        [SwaggerResponse(200, "Return export employees to excel.")]
        [SwaggerResponse(204, "Employee list not content.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> ExportAsync(string searchString = "") =>
            Ok(await Mediator.Send(new ExportEmployeesQuery(searchString)));
    }
}
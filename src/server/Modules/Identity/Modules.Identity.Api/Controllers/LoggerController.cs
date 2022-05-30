// --------------------------------------------------------------------------------------------------
// <copyright file="LoggerController.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Infrastructure.Permissions;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.DTO.Identity.Logging;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HureIT.Modules.Identity.Api.Controllers
{
    [ApiVersion("1")]
    internal sealed class LoggerController : BaseController
    {
        private readonly ILoggerService _eventLog;

        public LoggerController(ILoggerService eventLog)
        {
            _eventLog = eventLog;
        }

        [HttpGet]
        [HavePermission(PermissionsConstant.Logs.ViewAll)]
        [SwaggerOperation(
            Summary = "Get Log User List.",
            Description = "List all logs user in the database. This can only be done by the registered user",
            OperationId = "GetAllAsync")]
        [SwaggerResponse(200, "Return log user list.")]
        [SwaggerResponse(204, "Log user list not content.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedLogFilter filter)
        {
            var request = Mapper.Map<GetAllLogsRequest>(filter);
            var response = await _eventLog.GetAllAsync(request);
            return Ok(response);
        }
    }
}
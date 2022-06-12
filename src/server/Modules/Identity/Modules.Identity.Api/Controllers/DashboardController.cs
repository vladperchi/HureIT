// --------------------------------------------------------------------------------------------------
// <copyright file="DashboardController.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Interfaces.Services.Dashboard;
using HureIT.Shared.Infrastructure.Permissions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HureIT.Modules.Identity.Api.Controllers
{
    [ApiVersion("1")]
    internal sealed class DashboardController : BaseController
    {
        private readonly IStatsService _statsService;

        public DashboardController(IStatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        [HavePermission(PermissionsConstant.Dashboards.View)]
        [SwaggerOperation(
            Summary = "Get Stats.",
            Description = "All stats in the database. This can only be done by the registered user",
            OperationId = "GetStatsAsync")]
        [SwaggerResponse(200, "Return stats.")]
        [SwaggerResponse(204, "Stats not content.")]
        [SwaggerResponse(500, "Identity Internal Server Error.")]
        [SwaggerResponse(401, "No authorization to access.")]
        [SwaggerResponse(403, "No permission to access.")]
        public async Task<IActionResult> GetStatsDataAsync()
        {
            var response = await _statsService.GetStatsDataAsync();
            return Ok(response);
        }
    }
}
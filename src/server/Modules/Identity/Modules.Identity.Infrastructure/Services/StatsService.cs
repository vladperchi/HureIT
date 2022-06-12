// --------------------------------------------------------------------------------------------------
// <copyright file="StatsService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HureIT.Modules.Identity.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Shared.Core.Interfaces.Services.Dashboard;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Infrastructure.Services
{
    public class StatsService : IStatsService
    {
        private readonly IIdentityDbContext _contextIdentity;
        private readonly IWorkflowDbContext _contextWorkflow;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IEmployeeService _employeeService;
        private readonly IPermitTypeService _permitTypeService;
        private readonly IPermitService _permitService;
        private readonly IStringLocalizer<StatsService> _localizer;

        public StatsService(
            IIdentityDbContext contextIdentity,
            IWorkflowDbContext contextWorkflow,
            IUserService userService,
            IRoleService roleService,
            IEmployeeService employeeService,
            IPermitTypeService permitTypeService,
            IPermitService permitService,
            IStringLocalizer<StatsService> localizer)
        {
            _contextIdentity = contextIdentity;
            _contextWorkflow = contextWorkflow;
            _userService = userService;
            _roleService = roleService;
            _employeeService = employeeService;
            _permitTypeService = permitTypeService;
            _permitService = permitService;
            _localizer = localizer;
        }

        public async Task<IResult<GetStatsDataResponse>> GetStatsDataAsync()
        {
            var stats = new GetStatsDataResponse
            {
                EmployeeCount = await _employeeService.GetCountAsync(),
                PermitTypeCount = await _permitTypeService.GetCountAsync(),
                PermitCount = await _permitService.GetCountAsync(),
                UserCount = await _userService.GetCountAsync(),
                RoleCount = await _roleService.GetCountAsync()
            };

            int selectedYear = DateTime.Now.Year;
            double[] employeesFigure = new double[13];
            double[] permitTypesFigure = new double[13];
            double[] permitsFigure = new double[13];
            double[] usersFigure = new double[13];
            for (int i = 1; i <= 12; i++)
            {
                int month = i;
                var filterStartDate = new DateTime(selectedYear, month, 01);
                var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59);

                employeesFigure[i - 1] = await _contextWorkflow.Employees.Where(x => x.CreatedDate >= filterStartDate && x.CreatedDate <= filterEndDate).CountAsync();

                permitTypesFigure[i - 1] = await _contextWorkflow.PermitTypes.Where(x => x.CreatedDate >= filterStartDate && x.CreatedDate <= filterEndDate).CountAsync();

                permitsFigure[i - 1] = await _contextWorkflow.Permits.Where(x => x.CreatedDate >= filterStartDate && x.CreatedDate <= filterEndDate).CountAsync();

                usersFigure[i - 1] = await _contextIdentity.Users.Where(x => x.CreatedOn >= filterStartDate && x.CreatedOn <= filterEndDate).CountAsync();
            }

            stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Employees"], Data = employeesFigure });
            stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Permit Types"], Data = permitTypesFigure });
            stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Permits"], Data = permitsFigure });
            stats.DataEnterBarChart.Add(new ChartSeries { Name = _localizer["Users"], Data = usersFigure });

            return await Result<GetStatsDataResponse>.SuccessAsync(stats);
        }
    }
}
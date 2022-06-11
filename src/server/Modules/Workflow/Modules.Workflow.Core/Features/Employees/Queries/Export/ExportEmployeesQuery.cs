// --------------------------------------------------------------------------------------------------
// <copyright file="ExportEmployeesQuery.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Modules.Workflow.Core.Features.Permits.Queries.Export;
using HureIT.Modules.Workflow.Core.Specifications;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Queries.Export
{
    public class ExportEmployeesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportEmployeesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportEmployeesQueryHandler : IRequestHandler<ExportEmployeesQuery, Result<string>>
    {
        private readonly IWorkflowDbContext _context;
        private readonly IExcelService _excelService;
        private readonly ILogger<ExportEmployeesQueryHandler> _logger;
        private readonly IStringLocalizer<ExportEmployeesQueryHandler> _localizer;

        public ExportEmployeesQueryHandler(
            IExcelService excelService,
            IWorkflowDbContext context,
            ILogger<ExportEmployeesQueryHandler> logger,
            IStringLocalizer<ExportEmployeesQueryHandler> localizer)
        {
            _context = context;
            _excelService = excelService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportEmployeesQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new EmployeeFilterSpecification(request.SearchString);
            var data = await _context.Employees.AsNoTracking().AsQueryable().Specify(filterSpec).ToListAsync(cancellationToken);
            _ = data ?? throw new EmployeeListEmptyException(_localizer);
            string result = await _excelService.ExportAsync(data, mappers: new Dictionary<string, Func<Employee, object>>
            {
                { _localizer["Name"], item => item.FullName },
                { _localizer["Email"], item => item.Email.ToLower() },
                { _localizer["PhoneNumber"], item => item.PhoneNumber },
                { _localizer["Gender"], item => item.Gender.ToUpper() },
                { _localizer["Birthday"], item => item.Birthday.ToString("G", CultureInfo.CurrentCulture) },
                { _localizer["Active"], item => item.IsActive ? "YES" : "NO" },
                { _localizer["Admission Date"], item => item.AdmissionDate.ToString("G", CultureInfo.CurrentCulture) }
            }, sheetName: _localizer["Employee List"]);
            _logger.LogInformation(_localizer["Exported Details Employee List To Excel."]);
            return await Result<string>.SuccessAsync(data: result);
        }
    }
}
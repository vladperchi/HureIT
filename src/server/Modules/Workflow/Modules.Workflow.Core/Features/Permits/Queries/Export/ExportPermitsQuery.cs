// --------------------------------------------------------------------------------------------------
// <copyright file="ExportPermitsQuery.cs" company="HureIT">
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
using HureIT.Modules.Workflow.Core.Specifications;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Queries.Export
{
    public class ExportPermitsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportPermitsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportPermitsQueryHandler : IRequestHandler<ExportPermitsQuery, Result<string>>
    {
        private readonly IWorkflowDbContext _context;
        private readonly IExcelService _excelService;
        private readonly ILogger<ExportPermitsQueryHandler> _logger;
        private readonly IStringLocalizer<ExportPermitsQueryHandler> _localizer;

        public ExportPermitsQueryHandler(
            IExcelService excelService,
            IWorkflowDbContext context,
            ILogger<ExportPermitsQueryHandler> logger,
            IStringLocalizer<ExportPermitsQueryHandler> localizer)
        {
            _context = context;
            _excelService = excelService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportPermitsQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new PermitFilterSpecification(request.SearchString);
            var data = await _context.Permits.AsNoTracking().AsQueryable().Specify(filterSpec).ToListAsync(cancellationToken);
            _ = data ?? throw new PermitListEmptyException(_localizer);
            try
            {
                string result = await _excelService.ExportAsync(data, mappers: new Dictionary<string, Func<Permit, object>>
                {
                    { _localizer["Employee"], item => item.Employee.FullName.ToUpper() },
                    { _localizer["Permission"], item => item.PermitType.PermitName },
                    { _localizer["Description"], item => item.PermitType.Description },
                    { _localizer["StartDate"], item => item.StartDatePermit.ToString("G", CultureInfo.CurrentCulture) },
                    { _localizer["EndDate"], item => item.StartDatePermit.ToString("G", CultureInfo.CurrentCulture) }
                }, sheetName: _localizer["Assigned Permission List"]);
                _logger.LogInformation(_localizer["Exported Details Assigned Permission List To Excel."]);
                return await Result<string>.SuccessAsync(data: result);
            }
            catch (Exception)
            {
                throw new PermitCustomException(_localizer, null);
            }
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="ExportPermitTypesQuery.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
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

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries.Export
{
    public class ExportPermitTypesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportPermitTypesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportPermitTypesQueryHandler : IRequestHandler<ExportPermitTypesQuery, Result<string>>
    {
        private readonly IWorkflowDbContext _context;
        private readonly IExcelService _excelService;
        private readonly ILogger<ExportPermitTypesQueryHandler> _logger;
        private readonly IStringLocalizer<ExportPermitTypesQueryHandler> _localizer;

        public ExportPermitTypesQueryHandler(
            IExcelService excelService,
            IWorkflowDbContext context,
            ILogger<ExportPermitTypesQueryHandler> logger,
            IStringLocalizer<ExportPermitTypesQueryHandler> localizer)
        {
            _context = context;
            _excelService = excelService;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportPermitTypesQuery request, CancellationToken cancellationToken)
        {
            var filterSpec = new PermitTypeFilterSpecification(request.SearchString);
            var data = await _context.PermitTypes.AsNoTracking().AsQueryable().Specify(filterSpec).ToListAsync(cancellationToken);
            _ = data ?? throw new PermitTypeListEmptyException(_localizer);
            string result = await _excelService.ExportAsync(data, mappers: new Dictionary<string, Func<PermitType, object>>
            {
                { _localizer["Name"], item => item.PermitName },
                { _localizer["Description"], item => item.Description },
                { _localizer["Enabled"], item => item.IsEnabled ? "Yes" : "No" }
            }, sheetName: _localizer["Permission Type List"]);
            _logger.LogInformation(_localizer["Exported Details Permission Type List To Excel."]);
            return await Result<string>.SuccessAsync(data: result);
        }
    }
}
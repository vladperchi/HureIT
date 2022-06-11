// --------------------------------------------------------------------------------------------------
// <copyright file="PermitQueryHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Permits;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Extensions;
using HureIT.Modules.Workflow.Core.Specifications;

namespace HureIT.Modules.Workflow.Core.Features.Permits.Queries
{
    internal class PermitQueryHandler :
        IRequestHandler<GetAllPermitsQuery, PaginatedResult<GetAllPermitsResponse>>,
        IRequestHandler<GetPermitByIdQuery, Result<GetPermitByIdResponse>>
    {
        private readonly IWorkflowDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PermitQueryHandler> _localizer;

        public PermitQueryHandler(
            IWorkflowDbContext context,
            IMapper mapper,
            IStringLocalizer<PermitQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPermitsResponse>> Handle(GetAllPermitsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Permit, GetAllPermitsResponse>> expression = e => new GetAllPermitsResponse(e.Id, e.EmployeeId, e.PermitTypeId, e.StartDatePermit, e.EndDatePermit, e.Employee.FullName, e.PermitType.PermitName, e.PermitType.PermitDescription, e.StartDate, e.EndDate);
            var source = _context.Permits.AsNoTracking().AsQueryable();
            source = source.Include(x => x.Employee)
                           .Include(x => x.PermitType);
            if (request.EmployeeIds.Length > 0)
            {
                source = source.Where(x => request.EmployeeIds.Contains(x.EmployeeId));
            }

            if (request.PermitTypesIds.Length > 0)
            {
                source = source.Where(x => request.PermitTypesIds.Contains(x.PermitTypeId));
            }

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            source = !string.IsNullOrWhiteSpace(ordering) ? source.OrderBy(ordering) : source.OrderBy(x => x.Id);
            var filterSpec = new PermitFilterSpecification(request.SearchString);
            var data = await source.AsNoTracking().Specify(filterSpec).Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return data.Data.Count > 0
                ? _mapper.Map<PaginatedResult<GetAllPermitsResponse>>(data)
                : throw new PermitListEmptyException(_localizer);
        }

        public async Task<Result<GetPermitByIdResponse>> Handle(GetPermitByIdQuery query, CancellationToken cancellationToken)
        {
            var data = await _context.Permits.AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.PermitType)
                .Where(x => x.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            _ = data ?? throw new PermitNotFoundException(_localizer, query.Id);
            var result = _mapper.Map<GetPermitByIdResponse>(data);
            result.EmployeeName = data.Employee.FullName;
            result.PermitName = data.PermitType.PermitName;
            result.PermitDescription = data.PermitType.PermitDescription;
            return await Result<GetPermitByIdResponse>.SuccessAsync(result);
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeQueryHandler.cs" company="HureIT">
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
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.PermitTypes;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using HureIT.Modules.Workflow.Core.Specifications;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries
{
    internal class PermitTypeQueryHandler :
        IRequestHandler<GetAllPermitTypesQuery, PaginatedResult<GetAllPermitTypesResponse>>,
        IRequestHandler<GetPermitTypeByIdQuery, Result<GetPermitTypeByIdResponse>>
    {
        private readonly IWorkflowDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PermitTypeQueryHandler> _localizer;

        public PermitTypeQueryHandler(
            IWorkflowDbContext context,
            IMapper mapper,
            IStringLocalizer<PermitTypeQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPermitTypesResponse>> Handle(GetAllPermitTypesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<PermitType, GetAllPermitTypesResponse>> expression = e => new GetAllPermitTypesResponse(e.Id, e.Name, e.Description, e.CodeInternal, e.IsEnabled);
            var sourse = _context.PermitTypes.AsNoTracking().OrderBy(x => x.Id).AsQueryable();
            string ordering = new OrderByConverter().Convert(request.OrderBy);
            sourse = !string.IsNullOrWhiteSpace(ordering) ? sourse.OrderBy(ordering) : sourse.OrderBy(x => x.Id);
            var filterSpec = new PermitTypeFilterSpecification(request.SearchString);
            var data = await sourse.AsNoTracking()
                .Specify(filterSpec).Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            _ = data ?? throw new PermitTypeListEmptyException(_localizer);
            return _mapper.Map<PaginatedResult<GetAllPermitTypesResponse>>(data);
        }

        public async Task<Result<GetPermitTypeByIdResponse>> Handle(GetPermitTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var data = await _context.PermitTypes.AsNoTracking().Where(x => x.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            _ = data ?? throw new PermitTypeNotFoundException(_localizer, query.Id);
            var result = _mapper.Map<GetPermitTypeByIdResponse>(data);
            return await Result<GetPermitTypeByIdResponse>.SuccessAsync(result);
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeQueryHandler.cs" company="HureIT">
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
using HureIT.Shared.DTO.Workflow.Employees;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using HureIT.Modules.Workflow.Core.Specifications;
using HureIT.Shared.Core.Interfaces.Services.Workflow;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Queries
{
    internal class EmployeeQueryHandler :
       IRequestHandler<GetAllEmployeesQuery, PaginatedResult<GetAllEmployeesResponse>>,
       IRequestHandler<GetEmployeeByIdQuery, Result<GetEmployeeByIdResponse>>,
       IRequestHandler<GetEmployeeImageQuery, Result<string>>,
       IRequestHandler<GetEmployeesWithPermitsQuery, PaginatedResult<GetEmployeesWithPermitsResponse>>
    {
        private readonly IWorkflowDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPermitService _permitService;
        private readonly IStringLocalizer<EmployeeQueryHandler> _localizer;

        public EmployeeQueryHandler(
            IWorkflowDbContext context,
            IMapper mapper,
            IPermitService permitService,
            IStringLocalizer<EmployeeQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _permitService = permitService;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllEmployeesResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Employee, GetAllEmployeesResponse>> expression = e => new GetAllEmployeesResponse(e.Id, e.FullName, e.Admission, e.IsActive);
            var sourse = _context.Employees.AsNoTracking().OrderBy(x => x.Id).AsQueryable();
            string ordering = new OrderByConverter().Convert(request.OrderBy);
            sourse = !string.IsNullOrWhiteSpace(ordering)
                ? sourse.OrderBy(ordering)
                : sourse.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var filterSpec = new EmployeeFilterSpecification(request.SearchString);
            var data = await sourse.Specify(filterSpec).Select(expression).AsNoTracking().ToPaginatedListAsync(request.PageNumber, request.PageSize);
            _ = data ?? throw new EmployeeListEmptyException(_localizer);
            return _mapper.Map<PaginatedResult<GetAllEmployeesResponse>>(data);
        }

        public async Task<Result<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
        {
            var data = await _context.Employees.AsNoTracking().Where(x => x.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            _ = data ?? throw new EmployeeNotFoundException(_localizer, query.Id);
            var result = _permitService.GetAssignedPermitsByEmployeeAsycn(data.Id);
            return await result;
        }

        public async Task<Result<string>> Handle(GetEmployeeImageQuery request, CancellationToken cancellationToken)
        {
            string data = await _context.Employees.AsNoTracking()
                .Where(c => c.Id == request.Id).Select(a => a.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            _ = data ?? throw new EmployeeNotFoundException(_localizer, request.Id);
            return await Result<string>.SuccessAsync(data);
        }

        public async Task<PaginatedResult<GetEmployeesWithPermitsResponse>> Handle(GetEmployeesWithPermitsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Employee, GetEmployeesWithPermitsResponse>> expression = e => new GetEmployeesWithPermitsResponse(e.Id, e.FullName, e.PhoneNumber, e.Email);
            var sourse = _context.Employees.AsNoTracking().OrderBy(x => x.Id).AsQueryable();
            string ordering = new OrderByConverter().Convert(request.OrderBy);
            sourse = !string.IsNullOrWhiteSpace(ordering) ? sourse.OrderBy(ordering) : sourse.OrderBy(x => x.Id);
            var filterSpec = new EmployeeFilterSpecification(request.SearchString);
            var data = await sourse.Specify(filterSpec).Select(expression).AsNoTracking().ToPaginatedListAsync(request.PageNumber, request.PageSize);
            _ = data ?? throw new EmployeeListEmptyException(_localizer);
            foreach (var item in data.Data)
            {
                var employeeResponse = await _permitService.GetNumberPermitsByEmployeeAsycn(item.Id);
                item.TotalPermits = employeeResponse.Data.TotalPermits;
                item.PermitList = employeeResponse.Data.PermitList;
            }

            return _mapper.Map<PaginatedResult<GetEmployeesWithPermitsResponse>>(data);
        }
    }
}
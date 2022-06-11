// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Exceptions;
using HureIT.Modules.Workflow.Core.Features.Employees.Commands;
using HureIT.Modules.Workflow.Core.Features.Employees.Queries;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Employees;
using HureIT.Shared.Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IWorkflowDbContext _context;
        private readonly IMediator _mediator;
        private readonly IPermitService _permitService;
        private readonly IStringLocalizer<EmployeeService> _localizer;

        public EmployeeService(
            IWorkflowDbContext context,
            IPermitService permitService,
            IStringLocalizer<EmployeeService> localizer,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _permitService = permitService;
            _localizer = localizer;
        }

        public async Task<Result<GetEmployeeByIdResponse>> GetDetailsAsync(Guid id)
        {
            return await _mediator.Send(new GetEmployeeByIdQuery(id, false));
        }

        public async Task<Result<Guid>> RemoveAsync(Guid id)
        {
            return await _mediator.Send(new RemoveEmployeeCommand(id));
        }

        public async Task<string> GenerateFileName(int length) => await Utilities.GenerateCode("B", length);

        public Task<bool> GreaterThan18(DateTime value) =>
            Task.FromResult(DateTime.Now.AddYears(-18) >= value);

        public async Task<Result<string>> GetPictureAsync(Guid id)
        {
            string result = await _context.Employees.AsNoTracking().Where(x => x.Id == id).Select(a => a.ImageUrl).FirstOrDefaultAsync();
            _ = result ?? throw new EmployeeNotFoundException(_localizer, id);
            return await Result<string>.SuccessAsync(result);
        }

        public Task<bool> IsEmailUniqueAsync(string email)
        {
            bool isEmailUnique = _context.Employees.Any(x => x.Email.Equals(email.Trim().Normalize()));
            return Task.FromResult(isEmailUnique);
        }

        public Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber)
        {
            bool existsPhone = _context.Employees.Any(x => x.PhoneNumber.Equals(phoneNumber));
            return Task.FromResult(existsPhone);
        }

        public Task<bool> IsEmployeeActiveAsyn(Guid id)
        {
            bool existsPhone = _context.Employees.Any(x => x.Id.Equals(id) && x.IsActive);
            return Task.FromResult(existsPhone);
        }

        public async Task<int> GetCountAsync() => await _context.Employees.CountAsync();
    }
}
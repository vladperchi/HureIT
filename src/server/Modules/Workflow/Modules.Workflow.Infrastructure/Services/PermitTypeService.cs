// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using HureIT.Modules.Workflow.Core.Abstractions;
using HureIT.Modules.Workflow.Core.Features.Employees.Queries;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries;
using HureIT.Shared.Core.Interfaces.Services.Workflow;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.Employees;
using HureIT.Shared.DTO.Workflow.PermitTypes;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Workflow.Infrastructure.Services
{
    public class PermitTypeService : IPermitTypeService
    {
        private readonly IWorkflowDbContext _context;
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<PermitTypeService> _localizer;

        public PermitTypeService(
            IWorkflowDbContext context,
            IStringLocalizer<PermitTypeService> localizer,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
            _localizer = localizer;
        }

        public async Task<Result<GetPermitTypeByIdResponse>> GetDetailsAsync(Guid id)
        {
            return await _mediator.Send(new GetPermitTypeByIdQuery(id, false));
        }

        public async Task<Result<Guid>> RemoveAsync(Guid id)
        {
           return await _mediator.Send(new RemovePermitTypeCommand(id));
        }

        public Task<bool> IsNameUnique(string name)
        {
            bool matches = _context.PermitTypes.Any(e => e.Name.Equals(name));
            return Task.FromResult(matches);
        }

        public Task<bool> ExistsWithCode(string codeInternal)
        {
            bool matches = _context.PermitTypes.Any(x => x.CodeInternal.Equals(codeInternal));
            return Task.FromResult(matches);
        }

        public Task<bool> ExistsWithDescription(string description)
        {
            bool matches = _context.PermitTypes.Any(x => x.Description.Equals(description));
            return Task.FromResult(matches);
        }

        public async Task<int> GetCountAsync() => await _context.PermitTypes.CountAsync();
    }
}
﻿// --------------------------------------------------------------------------------------------------
// <copyright file="LoggerService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Shared.Core.Logging;
using HureIT.Shared.Core.Exceptions;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Core.Interfaces.Contexts;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Identity.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace HureIT.Shared.Infrastructure.Services
{
    /// <inheritdoc cref = "ILoggerService" />
    public class LoggerService : ILoggerService
    {
        private readonly IEventLogger _logger;
        private readonly IApplicationDbContext _dbContext;
        private readonly IStringLocalizer<LoggerService> _localizer;
        private readonly IMapper _mapper;

        public LoggerService(
            IApplicationDbContext dbContext,
            IStringLocalizer<LoggerService> localizer,
            IMapper mapper,
            IEventLogger logger)
        {
            _dbContext = dbContext;
            _localizer = localizer;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<EventLog>> GetAllAsync(GetAllLogsRequest request)
        {
            var queryable = _dbContext.EventLogs.AsNoTracking().AsQueryable();

            if (request.UserId != Guid.Empty)
            {
                queryable = queryable.Where(x => x.UserId.Equals(request.UserId));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Email.ToLower(), $"%{request.Email.ToLower()}%"));
            }

            if (!string.IsNullOrWhiteSpace(request.MessageType))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.MessageType.ToLower(), $"%{request.MessageType.ToLower()}%"));
            }

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderByDescending(a => a.Timestamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                string lowerSearchString = request.SearchString.ToLower();
                queryable = queryable.Where(x => (!string.IsNullOrWhiteSpace(x.Data)
                && EF.Functions.Like(x.Data.ToLower(), $"%{lowerSearchString}%")) || (!string.IsNullOrWhiteSpace(x.OldValues)
                && EF.Functions.Like(x.OldValues.ToLower(), $"%{lowerSearchString}%")) || (!string.IsNullOrWhiteSpace(x.NewValues)
                && EF.Functions.Like(x.NewValues.ToLower(), $"%{lowerSearchString}%")) || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{lowerSearchString}%"));
            }

            var eventLogList = await queryable.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            _ = eventLogList ?? throw new EventLogListEmptyException(_localizer);
            return eventLogList;
        }
    }
}
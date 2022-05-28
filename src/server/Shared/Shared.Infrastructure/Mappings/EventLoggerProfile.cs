// --------------------------------------------------------------------------------------------------
// <copyright file="EventLoggerProfile.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using HureIT.Shared.Core.Logging;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.DTO.Identity.Logging;

namespace HureIT.Shared.Infrastructure.Mappings
{
    public class EventLoggerProfile : Profile
    {
        public EventLoggerProfile()
        {
            CreateMap<PaginatedLogFilter, GetAllLogsRequest>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<LogRequest, EventLog>().ReverseMap();
        }
    }
}
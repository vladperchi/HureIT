// --------------------------------------------------------------------------------------------------
// <copyright file="PermitProfile.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Features.Permits.Commands;
using HureIT.Modules.Workflow.Core.Features.Permits.Queries;
using HureIT.Shared.Core.Features.Filters;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.DTO.Workflow.Permits;

namespace HureIT.Modules.Workflow.Core.Mappings
{
    public class PermitProfile : Profile
    {
        public PermitProfile()
        {
            CreateMap<AssignPermitCommand, Permit>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Permit>, GetPermitByIdQuery>();
            CreateMap<UpdatePermitCommand, Permit>().ReverseMap();
            CreateMap<GetAllPermitsResponse, Permit>().ReverseMap();
            CreateMap<GetPermitByIdResponse, Permit>().ReverseMap();
            CreateMap<PaginatedPermitFilter, GetAllPermitsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeProfile.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Commands;
using HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries;
using HureIT.Shared.Core.Features.Filters;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.DTO.Workflow.PermitTypes;

namespace HureIT.Modules.Workflow.Core.Mappings
{
    public class PermitTypeProfile : Profile
    {
        public PermitTypeProfile()
        {
            CreateMap<CreatePermitTypeCommand, PermitType>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, PermitType>, GetPermitTypeByIdQuery>();
            CreateMap<UpdatePermitTypeCommand, PermitType>().ReverseMap();
            CreateMap<GetAllPermitTypesResponse, PermitType>().ReverseMap();
            CreateMap<GetPermitTypeByIdResponse, PermitType>().ReverseMap();
            CreateMap<PaginatedPermitTypeFilter, GetAllPermitTypesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}
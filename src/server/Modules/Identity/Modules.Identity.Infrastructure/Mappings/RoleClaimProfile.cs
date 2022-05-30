// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimProfile.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.DTO.Identity.Roles;

namespace HureIT.Modules.Identity.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, HureRoleClaim>()
                .ForMember(nameof(HureRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(HureRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, HureRoleClaim>()
                .ForMember(nameof(HureRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(HureRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimModel, RoleClaimRequest>();
            CreateMap<RoleClaimModel, HureRoleClaim>().ReverseMap();
            CreateMap<RoleClaimModel, RoleClaimResponse>().ReverseMap();
        }
    }
}
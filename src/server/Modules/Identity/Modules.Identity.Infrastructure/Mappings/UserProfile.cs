// --------------------------------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Shared.DTO.Identity.Users;

namespace HureIT.Modules.Identity.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserResponse, HureUser>().ReverseMap();
            CreateMap<UpdateUserRequest, HureUser>().ReverseMap();
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeProfile.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using AutoMapper;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Core.Features.Employees.Commands;
using HureIT.Modules.Workflow.Core.Features.Employees.Queries;
using HureIT.Shared.Core.Features.Filters;
using HureIT.Shared.Core.Mappings.Converters;
using HureIT.Shared.DTO.Workflow.Employees;

namespace HureIT.Modules.Workflow.Core.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<RegisterEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetByIdCacheableFilter<Guid, Employee>, GetEmployeeByIdQuery>();
            CreateMap<UpdateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetAllEmployeesResponse, Employee>().ReverseMap();
            CreateMap<GetEmployeeByIdResponse, Employee>().ReverseMap();
            CreateMap<GetEmployeesWithPermitsResponse, Employee>().ReverseMap();
            CreateMap<PaginatedEmployeeFilter, GetAllEmployeesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
            CreateMap<PaginatedEmployeeFilter, GetEmployeesWithPermitsQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}
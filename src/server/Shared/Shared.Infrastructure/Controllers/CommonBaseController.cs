// --------------------------------------------------------------------------------------------------
// <copyright file="CommonBaseController.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using AutoMapper;
using HureIT.Shared.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HureIT.Shared.Infrastructure.Controllers
{
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class CommonBaseController : ControllerBase
    {
        protected internal const string BasePath = StringKeys.VersionPath;

        private IMediator _mediatorInstance;
        private IMapper _mapperInstance;
        private ILogger _loggerInstance;

        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();

        protected ILogger _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger>();
    }
}
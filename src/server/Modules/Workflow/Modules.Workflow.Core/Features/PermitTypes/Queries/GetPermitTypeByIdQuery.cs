// --------------------------------------------------------------------------------------------------
// <copyright file="GetPermitTypeByIdQuery.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Queries;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Workflow.PermitTypes;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Queries
{
    public class GetPermitTypeByIdQuery : IRequest<Result<GetPermitTypeByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }

        public bool SkipCache { get; protected set; }

        public string CacheKey { get; protected set; }

        public TimeSpan? Expiration { get; protected set; }

        public GetPermitTypeByIdQuery(Guid id, bool skipCache = false, TimeSpan? expiration = null)
        {
            Id = id;
            SkipCache = skipCache;
            CacheKey = CacheKeys.Common.GetEntityByIdCacheKey<Guid, PermitType>(id);
            Expiration = expiration;
        }
    }
}
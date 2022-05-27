// --------------------------------------------------------------------------------------------------
// <copyright file="GetByIdCacheableFilter.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Contracts;
using HureIT.Shared.Core.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HureIT.Shared.Core.Features.Filters
{
    public class GetByIdCacheableFilter<TEntityId, TEntity> : ICacheable
        where TEntity : class, IEntity<TEntityId>
    {
        [FromRoute(Name = "id")]
        public TEntityId Id { get; set; }

        [FromQuery]
        public bool SkipCache { get; set; }

        [FromQuery]
        public string CacheKey => CacheKeys.Common.GetEntityByIdCacheKey<TEntityId, TEntity>(Id);

        [FromQuery]
        [RegularExpression(StringKeys.SchemaRegexPattern)]

        public TimeSpan? Expiration { get; set; }
    }
}
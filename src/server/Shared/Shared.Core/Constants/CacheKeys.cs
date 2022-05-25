// --------------------------------------------------------------------------------------------------
// <copyright file="CacheKeys.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Core.Contracts;
using HureIT.Shared.Core.Utilities;

namespace HureIT.Shared.Core.Constants
{
    public static class CacheKeys
    {
        public static class Common
        {
            public static string GetEntityByIdCacheKey<TEntityId, TEntity>(TEntityId id)
                where TEntity : class, IEntity<TEntityId>
            {
                return $"GetEntity-{typeof(TEntity).GetGenericTypeName()}-{id}";
            }
        }
    }
}
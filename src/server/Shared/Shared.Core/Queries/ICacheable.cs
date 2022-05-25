// --------------------------------------------------------------------------------------------------
// <copyright file="ICacheable.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.Core.Queries
{
    public interface ICacheable
    {
        bool SkipCache { get; }

        string CacheKey { get; }

        TimeSpan? Expiration { get; }
    }
}
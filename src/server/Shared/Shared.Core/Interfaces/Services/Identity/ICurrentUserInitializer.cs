// --------------------------------------------------------------------------------------------------
// <copyright file="ICurrentUserInitializer.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Security.Claims;

namespace HureIT.Shared.Core.Interfaces.Services.Identity
{
    public interface ICurrentUserInitializer
    {
        void SetCurrentUserId(string userId);

        void SetCurrentUser(ClaimsPrincipal user);
    }
}
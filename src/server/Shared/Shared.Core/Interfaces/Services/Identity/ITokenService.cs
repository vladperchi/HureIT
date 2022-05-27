// --------------------------------------------------------------------------------------------------
// <copyright file="ITokenService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Identity.Tokens;

namespace HureIT.Shared.Core.Interfaces.Services.Identity
{
    public interface ITokenService
    {
        Task<IResult<TokenUserResponse>> GetTokenAsync(TokenUserRequest request, string ipAddress);

        Task<IResult<TokenUserResponse>> RefreshTokenAsync(RefreshTokenUserRequest request, string ipAddress);
    }
}
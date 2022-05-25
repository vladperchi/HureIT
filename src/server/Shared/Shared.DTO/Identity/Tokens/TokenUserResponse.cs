// --------------------------------------------------------------------------------------------------
// <copyright file="TokenUserResponse.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.DTO.Identity.Tokens
{
    public record TokenUserResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
}
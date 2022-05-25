// --------------------------------------------------------------------------------------------------
// <copyright file="TokenUserRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.DTO.Identity.Tokens
{
    public record TokenUserRequest(string Email, string Password);
}
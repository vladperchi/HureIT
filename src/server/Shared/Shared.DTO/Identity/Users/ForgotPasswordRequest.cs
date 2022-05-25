// --------------------------------------------------------------------------------------------------
// <copyright file="ForgotPasswordRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.DTO.Identity.Users
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="ClaimsPrincipalExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using System.Security.Claims;
using HureIT.Modules.Identity.Core.Exceptions;
using HureIT.Shared.Core.Constants;

namespace HureIT.Modules.Identity.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string GetUserPhoneNumber(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimTypes.MobilePhone);
            return claim?.Value;
        }

        public static string GetUserFirstName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimTypes.Name);
            return claim?.Value;
        }

        public static string GetuserSurname(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimTypes.Surname);
            return claim?.Value;
        }

        public static string GetFullName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimConstant.Fullname);
            return claim?.Value;
        }

        public static string GetImageUrl(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new IdentityException("User was not found...", statusCode: HttpStatusCode.NotFound);
            }

            var claim = principal.FindFirst(ClaimConstant.ImageUrl);
            return claim?.Value;
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="IdentityResultExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace HureIT.Modules.Identity.Infrastructure.Extensions
{
    internal static class IdentityResultExtensions
    {
        public static List<string> GetErrorMessages(this IdentityResult result, IStringLocalizer localizer) =>
            result.Errors.Select(x => localizer[x.Description].ToString()).ToList();
    }
}
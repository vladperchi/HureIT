// --------------------------------------------------------------------------------------------------
// <copyright file="StringKeys.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.Core.Constants
{
    public static class StringKeys
    {
        public const string AppName = "HureIT";
        public const string VersionPath = "api/v{version:apiVersion}";
        public const string AssemblyName = "HureIT.Bootstrapper";
        public const string Code = "code";
        public const string UserId = "userId";
        public const string Token = "Token";
        public const string SecuritySchemeName = "Authorization";
        public const string SecuritySchemeFormat = "JWT";
        public const string SchemaType = "string";
        public const string SchemaRegexPattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$";
        public const string SchemaTimeSpan = "02:00:00";
        public const string CurrentUserAnonymous = "Anonymous";
        public const string CurrentUserGuest = "Guest";
        public const string ClaimsIssuer = "LOCAL AUTHORITY";
        public const string HiddenValue = "*******";
        public const string StaffImageGhost = "Files\\Images\\Staff\\ghost.png";
        public const string EmployeeImageDefault = "Files\\Images\\Employees\\default.png";
        public const string PatternNumber = " ({0})";
        public const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    }
}
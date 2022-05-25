// --------------------------------------------------------------------------------------------------
// <copyright file="FileFormatInvalidException.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Net;
using Microsoft.Extensions.Localization;

namespace HureIT.Shared.Core.Exceptions
{
    public class FileFormatInvalidException : CustomException
    {
        public FileFormatInvalidException(IStringLocalizer localizer)
            : base(localizer["The file format not supported..."], null, HttpStatusCode.NotAcceptable)
        {
        }
    }
}
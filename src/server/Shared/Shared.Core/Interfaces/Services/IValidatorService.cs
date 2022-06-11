// --------------------------------------------------------------------------------------------------
// <copyright file="IValidatorService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace HureIT.Shared.Core.Interfaces.Services
{
    public interface IValidatorService
    {
        Task<bool> IsOnlyLetterAndSpace(string value);

        Task<bool> IsLetterOrDigit(string value);

        Task<bool> IsOnlyNumber(string value);

        Task<bool> IsValidDate(string date);
    }
}
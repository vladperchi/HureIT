// --------------------------------------------------------------------------------------------------
// <copyright file="ValidatorService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HureIT.Shared.Core.Interfaces.Services;

namespace HureIT.Shared.Infrastructure.Services
{
    public class ValidatorService : IValidatorService
    {
        public Task<bool> IsOnlyLetterAndSpace(string value) =>
             Task.FromResult(Regex.IsMatch(value, @"^(?! )[A-Za-z\s]+$"));

        public Task<bool> IsLetterOrDigit(string value) =>
            Task.FromResult(value.All(char.IsLetterOrDigit));

        public Task<bool> IsOnlyNumber(string value) =>
            Task.FromResult(value.All(char.IsNumber));

        public Task<bool> IsValidDate(string date) =>
            Task.FromResult(Regex.IsMatch(date, @"^(0[1-9]|1\d|2[0-8]|29(?=-\d\d-(?!1[01345789]00|2[1235679]00)\d\d(?:[02468][048]|[13579][26]))|30(?!-02)|31(?=-0[13578]|-1[02]))-(0[1-9]|1[0-2])-([12]\d{3}) ([01]\d|2[0-3]):([0-5]\d):([0-5]\d)$"));

        // Format Date: 02-06-2022 17:00:56
    }
}
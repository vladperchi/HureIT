// --------------------------------------------------------------------------------------------------
// <copyright file="ValidatorService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HureIT.Shared.Core.Interfaces.Services;

namespace ElevatorIT.Shared.Infrastructure.Services
{
    public class ValidatorService : IValidatorService
    {
        public Task<bool> IsOnlyLetterAndSpace(string value) =>
             Task.FromResult(Regex.IsMatch(value, @"^(?! )[A-Za-z\s]+$"));

        public Task<bool> IsLetterOrDigit(string value) =>
            Task.FromResult(value.All(char.IsLetterOrDigit));

        public Task<bool> IsOnlyNumber(int value) =>
            Task.FromResult(value.ToString().All(char.IsNumber));
    }
}
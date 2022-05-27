// --------------------------------------------------------------------------------------------------
// <copyright file="IExcelService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HureIT.Shared.Core.Interfaces.Services
{
    public interface IExcelService
    {
        Task<string> ExportAsync<TData>(IEnumerable<TData> data, Dictionary<string, Func<TData, object>> mappers, string sheetName);
    }
}
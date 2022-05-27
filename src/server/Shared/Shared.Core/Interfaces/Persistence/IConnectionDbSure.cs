// --------------------------------------------------------------------------------------------------
// <copyright file="IConnectionDbSure.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace HureIT.Shared.Core.Interfaces.Persistence
{
    public interface IConnectionDbSure
    {
        Task<string> MakeSure(string connectionString, string dataProvider = null);
    }
}
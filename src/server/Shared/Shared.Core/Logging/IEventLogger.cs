// --------------------------------------------------------------------------------------------------
// <copyright file="IEventLogger.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Core.Domain;

namespace HureIT.Shared.Core.Logging
{
    public interface IEventLogger
    {
        Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes)
            where T : Event;
    }
}
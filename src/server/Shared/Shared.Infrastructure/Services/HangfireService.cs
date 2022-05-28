// --------------------------------------------------------------------------------------------------
// <copyright file="HangfireService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HureIT.Shared.Core.Interfaces.Services;
using Hangfire;

namespace HureIT.Shared.Infrastructure.Services
{
    /// <inheritdoc cref = "IJobService" />
    public class HangfireService : IJobService
    {
        public string Enqueue(Expression<Func<Task>> methodCall) =>
            BackgroundJob.Enqueue(methodCall);
    }
}
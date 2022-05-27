// --------------------------------------------------------------------------------------------------
// <copyright file="IJobService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HureIT.Shared.Core.Interfaces.Services
{
    public interface IJobService
    {
        string Enqueue(Expression<Func<Task>> methodCall);
    }
}
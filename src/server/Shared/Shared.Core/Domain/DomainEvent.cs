// --------------------------------------------------------------------------------------------------
// <copyright file="DomainEvent.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace HureIT.Shared.Core.Domain
{
    public abstract class DomainEvent : Event
    {
        protected DomainEvent(Guid addedId)
        {
            AggregateId = addedId;
        }
    }
}
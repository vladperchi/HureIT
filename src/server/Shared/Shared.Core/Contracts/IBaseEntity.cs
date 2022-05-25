// --------------------------------------------------------------------------------------------------
// <copyright file="IBaseEntity.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using HureIT.Shared.Core.Domain;

namespace HureIT.Shared.Core.Contracts
{
    public interface IBaseEntity
    {
        public IReadOnlyCollection<Event> DomainEvents { get; }

        public void AddDomainEvent(Event domainEvent);

        public void RemoveDomainEvent(Event domainEvent);

        public void ClearDomainEvents();
    }
}
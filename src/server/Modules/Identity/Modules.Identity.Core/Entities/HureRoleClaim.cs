// --------------------------------------------------------------------------------------------------
// <copyright file="HureRoleClaim.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using HureIT.Shared.Core.Contracts;
using HureIT.Shared.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace HureIT.Modules.Identity.Core.Entities
{
    public class HureRoleClaim : IdentityRoleClaim<string>, IBaseEntity
    {
        public HureRoleClaim()
            : base()
        {
        }

        public HureRoleClaim(string roleClaimDescription = null, string roleClaimGroup = null)
            : base()
        {
            Description = roleClaimDescription;
            Group = roleClaimGroup;
        }

        public string Description { get; set; }

        public string Group { get; set; }

        public virtual HureRole Role { get; set; }

        private List<Event> _domainEvents;

        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents ??= new List<Event>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="HureRole.cs" company="HureIT">
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
    public class HureRole : IdentityRole, IEntity<string>, IBaseEntity
    {
        public HureRole()
            : base()
        {
            RoleClaims = new HashSet<HureRoleClaim>();
        }

        public HureRole(string roleName, string roleDescription = null)
            : base(roleName)
        {
            RoleClaims = new HashSet<HureRoleClaim>();
            Description = roleDescription;
        }

        public string Description { get; set; }

        public virtual ICollection<HureRoleClaim> RoleClaims { get; set; }

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
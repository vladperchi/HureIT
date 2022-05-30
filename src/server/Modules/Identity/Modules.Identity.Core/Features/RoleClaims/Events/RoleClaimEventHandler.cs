// --------------------------------------------------------------------------------------------------
// <copyright file="RoleClaimEventHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Identity.Core.Features.RoleClaims.Events
{
    public class RoleClaimEventHandler :
       INotificationHandler<RoleClaimAddedEvent>,
       INotificationHandler<RoleClaimUpdatedEvent>,
       INotificationHandler<RoleClaimDeletedEvent>
    {
        private readonly ILogger<RoleClaimEventHandler> _logger;
        private readonly IStringLocalizer<RoleClaimEventHandler> _localizer;

        public RoleClaimEventHandler(
            ILogger<RoleClaimEventHandler> logger,
            IStringLocalizer<RoleClaimEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(RoleClaimAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimAddedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(RoleClaimUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimUpdatedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(RoleClaimDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(RoleClaimDeletedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="PermitEventHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using HureIT.Modules.Workflow.Core.Features.Permits.Events;

namespace HureIT.Modules.Workflow.Core.Features.Elevators.Events
{
    public class PermitEventHandler :
        INotificationHandler<PermitAssignedEvent>,
        INotificationHandler<PermitUpdatedEvent>,
        INotificationHandler<PermitRemovedEvent>
    {
        private readonly ILogger<PermitEventHandler> _logger;
        private readonly IStringLocalizer<PermitEventHandler> _localizer;

        public PermitEventHandler(
            ILogger<PermitEventHandler> logger,
            IStringLocalizer<PermitEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(PermitAssignedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PermitAssignedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(PermitUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PermitUpdatedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(PermitRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PermitRemovedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }
    }
}
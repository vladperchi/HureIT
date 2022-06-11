// --------------------------------------------------------------------------------------------------
// <copyright file="PermitTypeEventHandler.cs" company="HureIT">
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

namespace HureIT.Modules.Workflow.Core.Features.PermitTypes.Events
{
    public class PermitTypeEventHandler :
        INotificationHandler<PermitTypeRegisteredEvent>,
        INotificationHandler<PermitTypeUpdatedEvent>,
        INotificationHandler<PermitTypeRemovedEvent>
    {
        private readonly ILogger<PermitTypeEventHandler> _logger;
        private readonly IStringLocalizer<PermitTypeEventHandler> _localizer;

        public PermitTypeEventHandler(
            ILogger<PermitTypeEventHandler> logger,
            IStringLocalizer<PermitTypeEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(PermitTypeRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PermitTypeRegisteredEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(PermitTypeUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PermitTypeUpdatedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(PermitTypeRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(PermitTypeRemovedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }
    }
}
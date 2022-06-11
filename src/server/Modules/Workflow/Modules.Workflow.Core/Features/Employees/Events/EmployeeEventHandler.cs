// --------------------------------------------------------------------------------------------------
// <copyright file="EmployeeEventHandler.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using MediatR;

namespace HureIT.Modules.Workflow.Core.Features.Employees.Events
{
    public class EmployeeEventHandler :
        INotificationHandler<EmployeeRegisteredEvent>,
        INotificationHandler<EmployeeUpdatedEvent>,
        INotificationHandler<EmployeeRemovedEvent>
    {
        private readonly ILogger<EmployeeEventHandler> _logger;
        private readonly IStringLocalizer<EmployeeEventHandler> _localizer;

        public EmployeeEventHandler(
            ILogger<EmployeeEventHandler> logger,
            IStringLocalizer<EmployeeEventHandler> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public Task Handle(EmployeeRegisteredEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(EmployeeRegisteredEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(EmployeeUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(EmployeeUpdatedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }

        public Task Handle(EmployeeRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer[$"{nameof(EmployeeRemovedEvent)} High. {notification.EventDescription}"]);
            return Task.CompletedTask;
        }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="EventLogger.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Core.Domain;
using HureIT.Shared.Core.Logging;
using HureIT.Shared.Core.Interfaces.Contexts;
using HureIT.Shared.Core.Interfaces.Services.Identity;
using HureIT.Shared.Core.Interfaces.Serialization.Serializer;
using HureIT.Shared.Core.Constants;

namespace HureIT.Shared.Infrastructure.Logging
{
    internal class EventLogger : IEventLogger
    {
        private readonly ICurrentUser _currentUser;
        private readonly IApplicationDbContext _context;
        private readonly IJsonSerializer _jsonSerializer;

        public EventLogger(
            ICurrentUser user,
            IApplicationDbContext context,
            IJsonSerializer jsonSerializer)
        {
            _currentUser = user;
            _context = context;
            _jsonSerializer = jsonSerializer;
        }

        public async Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes)
            where T : Event
        {
            if (@event is EventLog eventLog)
            {
                await _context.EventLogs.AddAsync(eventLog);
                await _context.SaveChangesAsync();
            }
            else
            {
                var userId = _currentUser.GetUserId();
                string userEmail = !string.IsNullOrWhiteSpace(_currentUser.GetUserEmail())
                    ? _currentUser.GetUserEmail()
                    : StringKeys.CurrentUserAnonymous;
                string serializedData = _jsonSerializer.Serialize(@event, @event.GetType());
                var thisEventLog = new EventLog(
                    @event,
                    serializedData,
                    changes,
                    userEmail,
                    userId);
                await _context.EventLogs.AddAsync(thisEventLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
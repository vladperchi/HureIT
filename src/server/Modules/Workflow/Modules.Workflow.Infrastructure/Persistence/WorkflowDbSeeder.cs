// --------------------------------------------------------------------------------------------------
// <copyright file="WorkflowDbSeeder.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HureIT.Modules.Workflow.Core.Entities;
using HureIT.Modules.Workflow.Infrastructure.Persistence;
using HureIT.Modules.Workflow.Infrastructure.Persistence.Resources;
using HureIT.Shared.Core.Interfaces.Serialization.Serializer;
using HureIT.Shared.Core.Interfaces.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace HureIT.Modules.Workflow.Infrastructure.Persistence
{
    public class WorkflowDbSeeder : IDbSeeder
    {
        private readonly ILogger<WorkflowDbSeeder> _logger;
        private readonly WorkflowDbContext _context;
        private readonly IStringLocalizer<WorkflowDbSeeder> _localizer;
        private readonly IJsonSerializer _jsonSerializer;

        public WorkflowDbSeeder(
            ILogger<WorkflowDbSeeder> logger,
            WorkflowDbContext context,
            IStringLocalizer<WorkflowDbSeeder> localizer,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _context = context;
            _localizer = localizer;
            _jsonSerializer = jsonSerializer;
        }

        public void Initialize()
        {
            try
            {
                AddEmpoyees();
                AddPermitTypes();
                AddPermits();
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(_localizer[$"An error occurred while seeding data workflow module. Exception:{ex.Message}"]);
            }
        }

        private void AddEmpoyees()
        {
            Task.Run(async () =>
            {
                if (!_context.Employees.Any())
                {
                    var dataDeserialize = DeserializeJson<Employee>(Seeds.Employees);
                    if (dataDeserialize != null)
                    {
                        foreach (var item in dataDeserialize)
                        {
                            await _context.Employees.AddAsync(item);
                        }

                        await _context.SaveChangesAsync();
                        _logger.LogInformation(_localizer["Seeded Employees Successfully."]);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddPermitTypes()
        {
            Task.Run(async () =>
            {
                if (!_context.PermitTypes.Any())
                {
                    var dataDeserialize = DeserializeJson<PermitType>(Seeds.PermitTypes);
                    if (dataDeserialize != null)
                    {
                        foreach (var item in dataDeserialize)
                        {
                            await _context.PermitTypes.AddAsync(item);
                        }

                        await _context.SaveChangesAsync();
                        _logger.LogInformation(_localizer["Seeded Permit Types Successfully."]);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddPermits()
        {
            Task.Run(async () =>
            {
                if (!_context.Permits.Any())
                {
                    var dataDeserialize = DeserializeJson<Permit>(Seeds.Permits);
                    if (dataDeserialize != null)
                    {
                        foreach (var item in dataDeserialize)
                        {
                            await _context.Permits.AddAsync(item);
                        }

                        await _context.SaveChangesAsync();
                        _logger.LogInformation(_localizer["Seeded Permits Successfully."]);
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private List<T> DeserializeJson<T>(byte[] data)
            => _jsonSerializer.Deserialize<List<T>>(Encoding.Default.GetString(data));
    }
}
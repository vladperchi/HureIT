// --------------------------------------------------------------------------------------------------
// <copyright file="ModelBuilderExtensions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;

namespace HureIT.Shared.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyApplicationConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // Set column type (Float, Decimal, DateTime, etc)
        }

        public static void ApplyModuleConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // Build model for MSSQL
        }
    }
}
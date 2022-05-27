// --------------------------------------------------------------------------------------------------
// <copyright file="HangfireStorageSettings.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.Core.Settings
{
    public class HangfireStorageSettings
    {
        public string StorageProvider { get; set; }

        public string ConnectionString { get; set; }

        public string Documentation { get; set; }
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="IJsonSerializerSettings.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace HureIT.Shared.Core.Interfaces.Serialization.Settings
{
    public interface IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; }
    }
}
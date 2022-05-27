// --------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializerSettingsOptions.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Text.Json;
using HureIT.Shared.Core.Interfaces.Serialization;
using Newtonsoft.Json;

namespace HureIT.Shared.Core.Serialization
{
    public class JsonSerializerSettingsOptions : IJsonSerializerSettingsOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();

        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}
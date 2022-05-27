﻿// --------------------------------------------------------------------------------------------------
// <copyright file="NewtonSoftJsonSerializer.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using HureIT.Shared.Core.Interfaces.Serialization;
using HureIT.Shared.Core.Interfaces.Serialization.Serializer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HureIT.Shared.Core.Serialization
{
    public class NewtonSoftJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonSoftJsonSerializer(IOptions<JsonSerializerSettingsOptions> settings)
        {
            _settings = settings.Value.JsonSerializerSettings;
        }

        public T Deserialize<T>(string data, IJsonSerializerSettingsOptions settings = null)
            => JsonConvert.DeserializeObject<T>(data, settings?.JsonSerializerSettings ?? _settings);

        public string Serialize<T>(T data, IJsonSerializerSettingsOptions settings = null)
            => JsonConvert.SerializeObject(data, settings?.JsonSerializerSettings ?? _settings);

        public string Serialize<T>(T data, Type type, IJsonSerializerSettingsOptions settings = null)
            => JsonConvert.SerializeObject(data, type, settings?.JsonSerializerSettings ?? _settings);
    }
}
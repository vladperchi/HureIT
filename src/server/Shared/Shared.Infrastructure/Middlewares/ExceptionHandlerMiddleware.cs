// --------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlerMiddleware.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Exceptions;
using HureIT.Shared.Core.Interfaces.Serialization.Serializer;
using HureIT.Shared.Core.Interfaces.Services.Identity;
using HureIT.Shared.Core.Serialization;
using HureIT.Shared.Core.Settings;
using HureIT.Shared.Core.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Context;

namespace HureIT.Shared.Infrastructure.Middlewares
{
    internal class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IStringLocalizer<ExceptionHandlerMiddleware> _localizer;
        private readonly SerializationSettings _serializationSettings;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ICurrentUser _currentUser;

        public ExceptionHandlerMiddleware(
            ILogger<ExceptionHandlerMiddleware> logger,
            IStringLocalizer<ExceptionHandlerMiddleware> localizer,
            IOptions<SerializationSettings> serializationSettings,
            IJsonSerializer jsonSerializer,
            ICurrentUser currentUser)
        {
            _logger = logger;
            _localizer = localizer;
            _serializationSettings = serializationSettings.Value;
            _jsonSerializer = jsonSerializer;
            _currentUser = currentUser;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                var request = httpContext.Request;
                string requestBody = string.Empty;
                if (request.Path.ToString().Contains("tokens"))
                {
                    requestBody = "[Redacted] Sensitive Information.";
                }
                else if (!string.IsNullOrEmpty(request.ContentType) && request.ContentType.StartsWith("application/json"))
                {
                    request.EnableBuffering();
                    Stream body = request.Body;
                    byte[] buffer = new byte[Convert.ToInt32(request.ContentLength)];
                    await body.ReadAsync(buffer);
                    using var reader = new StreamReader(body, Encoding.UTF8, true, buffer.Length, true);
                    requestBody = await reader.ReadToEndAsync();
                    body.Seek(0, SeekOrigin.Begin);
                    request.Body = body;
                }

                string email = _currentUser?.GetUserEmail() is string userEmail ? userEmail : StringKeys.CurrentUserAnonymous;
                LogContext.PushProperty("UserEmail", email);
                var userId = _currentUser.GetUserId();
                if (userId != Guid.Empty)
                {
                    LogContext.PushProperty("UserId", userId);
                    string fullName = _currentUser.Name is string name ? name : StringKeys.CurrentUserGuest;
                    LogContext.PushProperty("UserName", fullName);
                }

                string errorId = Guid.NewGuid().ToString();
                LogContext.PushProperty("ErrorId", errorId);
                var errorResult = new ErrorResult<string>
                {
                    ErrorId = errorId,
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    Host = $"{request.Host}",
                    Path = $"{request.Path}",
                    RemoteIP = $"{httpContext.Connection.RemoteIpAddress}",
                    Schema = request.Scheme ?? string.Empty,
                    Method = request.Method ?? string.Empty,
                    SupportMessage = string.Format(_localizer["Please provide the ErrorId: {0} to the support team for further analysis."], errorId),
                };
                LogContext.PushProperty("RemoteIP", errorResult.RemoteIP);
                LogContext.PushProperty("Schema", errorResult.Schema);
                LogContext.PushProperty("Host", errorResult.Host);
                LogContext.PushProperty("Method", errorResult.Method);
                LogContext.PushProperty("Path", errorResult.Path);
                LogContext.PushProperty("RequestBody", requestBody);
                Log.ForContext("RequestHeaders", request.Headers
                    .ToDictionary(x => x.Key, x => x.Value.ToString()), destructureObjects: true)
                    .ForContext("RequestBody", requestBody)
                    .Information("HTTP {0} Request sent to {1}", errorResult.Method, errorResult.Path);
                errorResult.Messages!.Add(exception.Message);

                var response = httpContext.Response;
                string responseBody = string.Empty;
                var originalBody = httpContext.Response.Body;
                using var eBody = new MemoryStream();
                response.Body = eBody;
                if (request.Path.ToString().Contains("tokens"))
                {
                    requestBody = "[Redacted] Sensitive Information.";
                }
                else
                {
                    eBody.Seek(0, SeekOrigin.Begin);
                    responseBody = await new StreamReader(response.Body).ReadToEndAsync();
                }

                response.ContentType = "application/json";
                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                switch (exception)
                {
                    case CustomException ex:
                        response.StatusCode = errorResult.StatusCode = (int)ex.StatusCode;
                        errorResult.Messages = ex.ErrorMessages;
                        break;

                    case KeyNotFoundException:
                        response.StatusCode = errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = string.Empty;
                if (_serializationSettings.UseNewtonsoftJson)
                {
                    result = _jsonSerializer.Serialize(errorResult, new JsonSerializerSettingsOptions
                    {
                        JsonSerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    });
                }
                else if (_serializationSettings.UseSystemTextJson)
                {
                    result = _jsonSerializer.Serialize(errorResult, new JsonSerializerSettingsOptions
                    {
                        JsonSerializerOptions = { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    });
                }

                LogContext.PushProperty("StatusCode", response.StatusCode);
                LogContext.PushProperty("ResponseTime", DateTime.Now);
                Log.ForContext("ResponseHeaders", response.Headers
                    .ToDictionary(x => x.Key, x => x.Value.ToString()), destructureObjects: true)
                    .ForContext("ResponseBody", responseBody)
                    .Warning("HTTP Response Status Code {0}.", response.StatusCode);
                LogContext.PushProperty("StackTrace", exception.StackTrace);
                eBody.Seek(0, SeekOrigin.Begin);
                await eBody.CopyToAsync(originalBody);

                _logger.LogError($"{errorResult.Exception}:::Request failed with StatusCode:{response.StatusCode}::: {errorResult.SupportMessage}");
                await response.WriteAsync(result);
            }
        }
    }
}
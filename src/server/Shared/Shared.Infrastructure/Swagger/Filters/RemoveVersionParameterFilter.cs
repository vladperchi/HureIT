// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveVersionParameterFilter.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HureIT.Shared.Infrastructure.Swagger.Filters
{
    public class RemoveVersionParameterFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters.Count == 0)
            {
                return;
            }

            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
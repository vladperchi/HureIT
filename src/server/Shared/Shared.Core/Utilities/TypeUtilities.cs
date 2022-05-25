// --------------------------------------------------------------------------------------------------
// <copyright file="TypeUtilities.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace HureIT.Shared.Core.Utilities
{
    public static class TypeUtilities
    {
        public static string GetGenericTypeName(this Type type)
        {
            if (type.IsGenericType)
            {
                string genericTypes = string.Join(",", type.GetGenericArguments().Select(GetGenericTypeName).ToArray());
                return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }

            return type.Name;
        }
    }
}
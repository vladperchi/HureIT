// --------------------------------------------------------------------------------------------------
// <copyright file="UploadStorageType.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace HureIT.Shared.DTO.Upload
{
    public enum UploadStorageType
    {
        [Description(@"Images\User\Staff")]
        Staff,

        [Description(@"Images\Person\Employees")]
        Employee
    }
}
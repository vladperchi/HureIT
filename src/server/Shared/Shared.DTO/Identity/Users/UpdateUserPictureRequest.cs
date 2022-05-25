// --------------------------------------------------------------------------------------------------
// <copyright file="UpdateUserPictureRequest.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using HureIT.Shared.DTO.Upload;

namespace HureIT.Shared.DTO.Identity.Users
{
    public class UpdateUserPictureRequest : FileUploadRequest
    {
        public bool DeleteCurrentImageUrl { get; set; } = false;
    }
}
// --------------------------------------------------------------------------------------------------
// <copyright file="IUploadService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using HureIT.Shared.Core.Common.Enums;
using HureIT.Shared.DTO.Upload;

namespace HureIT.Shared.Core.Interfaces.Services
{
    public interface IUploadService
    {
        Task<string> UploadAsync(FileUploadRequest request, FileType supportedFileType);

        Task<bool> RemoveFileImage(UploadStorageType pathFolder, string currentImageUrl);
    }
}
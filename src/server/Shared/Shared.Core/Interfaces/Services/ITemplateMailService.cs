// --------------------------------------------------------------------------------------------------
// <copyright file="ITemplateMailService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace HureIT.Shared.Core.Interfaces.Services
{
    public interface ITemplateMailService
    {
        Task<string> GenerateEmailTemplate<T>(string name, T emailModel);
    }
}
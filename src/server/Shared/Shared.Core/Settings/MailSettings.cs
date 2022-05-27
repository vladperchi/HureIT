// --------------------------------------------------------------------------------------------------
// <copyright file="MailSettings.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

namespace HureIT.Shared.Core.Settings
{
    public class MailSettings
    {
        public string From { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public bool EnableVerification { get; set; }
    }
}
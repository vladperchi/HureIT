// --------------------------------------------------------------------------------------------------
// <copyright file="PermissionsConstant.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.ComponentModel;

namespace HureIT.Shared.Core.Constants
{
    public static class PermissionsConstant
    {
        [DisplayName("Employees")]
        [Description("Employees Permissions")]
        public static class Employees
        {
            public const string View = "Permissions.Employees.View";
            public const string ViewAll = "Permissions.Employees.ViewAll";
            public const string ViewList = "Permissions.Employees.ViewList";
            public const string Register = "Permissions.Employees.Register";
            public const string Update = "Permissions.Employees.Update";
            public const string Remove = "Permissions.Employees.Remove";
            public const string Export = "Permissions.Employees.Export";
        }

        [DisplayName("PermitTypes")]
        [Description("Permit Types Permissions")]
        public static class PermitTypes
        {
            public const string View = "Permissions.PermitTypes.View";
            public const string ViewAll = "Permissions.PermitTypes.ViewAll";
            public const string Create = "Permissions.PermitTypes.Create";
            public const string Update = "Permissions.PermitTypes.Update";
            public const string Remove = "Permissions.PermitTypes.Remove";
            public const string Export = "Permissions.PermitTypes.Export";
        }

        [DisplayName("WorkPermits")]
        [Description("Work Permits Permissions")]
        public static class WorkPermits
        {
            public const string View = "Permissions.WorkPermits.View";
            public const string ViewAll = "Permissions.WorkPermits.ViewAll";
            public const string Assign = "Permissions.WorkPermits.Assign";
            public const string Update = "Permissions.WorkPermits.Update";
            public const string Remove = "Permissions.WorkPermits.Remove";
            public const string Export = "Permissions.WorkPermits.Export";
        }

        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string ViewAll = "Permissions.Users.ViewAll";
            public const string Register = "Permissions.Users.Register";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Export = "Permissions.Users.Export";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string ViewAll = "Permissions.Roles.ViewAll";
            public const string AddOrUpdate = "Permissions.Roles.AddOrUpdate";
            public const string Delete = "Permissions.Roles.Delete";
        }

        [DisplayName("RoleClaims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
        }

        [DisplayName("Logs")]
        [Description("Logs Permissions")]
        public static class Logs
        {
            public const string ViewAll = "Permissions.Logs.ViewAll";
            public const string Create = "Permissions.Logs.Create";
        }

        [DisplayName("Dashboards")]
        [Description("Dashboards Permissions")]
        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        [DisplayName("Hangfire")]
        [Description("Hangfire Permissions")]
        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.ViewAll";
        }
    }
}
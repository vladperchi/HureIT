// --------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="HureIT">
// Copyright (c) HureIT. All rights reserved.
// Developer: Vladimir P. CHibás (vladperchi).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using HureIT.Modules.Identity.Core.Abstractions;
using HureIT.Modules.Identity.Core.Entities;
using HureIT.Modules.Identity.Core.Exceptions;
using HureIT.Modules.Identity.Core.Features.Users.Events;
using HureIT.Modules.Identity.Infrastructure.Extensions;
using HureIT.Modules.Identity.Infrastructure.Specifications;
using HureIT.Shared.Core.Common.Enums;
using HureIT.Shared.Core.Constants;
using HureIT.Shared.Core.Exceptions;
using HureIT.Shared.Core.Extensions;
using HureIT.Shared.Core.Interfaces.Services;
using HureIT.Shared.Core.Interfaces.Services.Identity;
using HureIT.Shared.Core.Settings;
using HureIT.Shared.Core.Wrapper;
using HureIT.Shared.DTO.Identity.Users;
using HureIT.Shared.DTO.Mails;
using HureIT.Shared.DTO.Upload;
using HureIT.Shared.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HureIT.Modules.Identity.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<HureUser> _userManager;
        private readonly RoleManager<HureRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UserService> _localizer;
        private readonly ILogger<UserService> _logger;
        private readonly IJobService _jobService;
        private readonly IMailService _mailService;
        private readonly MailSettings _mailSettings;
        private readonly ITemplateMailService _templateService;
        private readonly TemplateMailSettings _templateMailSettings;
        private readonly IExcelService _excelService;
        private readonly ILoggerService _eventLog;
        private readonly ICurrentUser _currentUser;
        private readonly IUploadService _uploadService;

        public UserService(
            UserManager<HureUser> userManager,
            RoleManager<HureRole> roleManager,
            IMapper mapper,
            IStringLocalizer<UserService> localizer,
            ILogger<UserService> logger,
            IJobService jobService,
            IOptions<MailSettings> mailSettings,
            IMailService mailService,
            IOptions<TemplateMailSettings> templateMailSettings,
            ITemplateMailService templateService,
            IExcelService excelService,
            ILoggerService eventLog,
            ICurrentUser currentUser,
            IUploadService uploadService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _localizer = localizer;
            _logger = logger;
            _jobService = jobService;
            _mailSettings = mailSettings.Value;
            _mailService = mailService;
            _templateMailSettings = templateMailSettings.Value;
            _templateService = templateService;
            _excelService = excelService;
            _eventLog = eventLog;
            _currentUser = currentUser;
            _uploadService = uploadService;
        }

        public async Task<Result<List<UserResponse>>> GetAllAsync()
        {
            var data = await _userManager.Users.AsNoTracking().ToListAsync();
            _ = data ?? throw new UserListEmptyException(_localizer);
            var result = _mapper.Map<List<UserResponse>>(data);
            return await Result<List<UserResponse>>.SuccessAsync(result);
        }

        public async Task<IResult<UserResponse>> GetByIdAsync(string userId)
        {
            var data = await _userManager.Users.AsNoTracking().Where(x => x.Id == userId).FirstOrDefaultAsync();
            _ = data ?? throw new UserNotFoundException(_localizer, userId);
            var result = _mapper.Map<UserResponse>(data);
            return await Result<UserResponse>.SuccessAsync(result);
        }

        public async Task<IResult<string>> GetPictureAsync(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            _ = result ?? throw new UserNotFoundException(_localizer, userId);
            return await Result<string>.SuccessAsync(data: result.ImageUrl);
        }

        public async Task<IResult<UserRolesResponse>> GetRolesByUserAsync(string userId)
        {
            var viewModel = new List<UserRoleModel>();
            var user = await _userManager.FindByIdAsync(userId);
            _ = user ?? throw new UserNotFoundException(_localizer, userId);
            try
            {
                var roles = await _roleManager.Roles.AsNoTracking().ToListAsync();
                foreach (var role in roles)
                {
                    var userRolesViewModel = new UserRoleModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                    userRolesViewModel.Selected = await _userManager.IsInRoleAsync(user, role.Name);
                    viewModel.Add(userRolesViewModel);
                }

                var result = new UserRolesResponse { UserRoles = viewModel };
                return await Result<UserRolesResponse>.SuccessAsync(result);
            }
            catch (Exception)
            {
                throw new IdentityCustomException(_localizer, null);
            }
        }

        public async Task<IResult> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithUserName != null)
            {
                throw new IdentityException(string.Format(_localizer["Username {0} is in use."], request.UserName));
            }

            var user = new HureUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,
                CreatedOn = DateTime.Now,
                CreatedBy = _currentUser.GetUserId().ToString(),
                ImageUrl = StringKeys.UserImageGhost
            };

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (await ExistsWithEmailAsync(request.Email))
                {
                    throw new IdentityException(string.Format(_localizer["Account Email {0} is registered."], request.Email));
                }
            }

            if (request.EmailConfirmed) user.EmailConfirmed = true;

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                if (await ExistsWithPhoneNumberAsync(request.PhoneNumber))
                {
                    throw new IdentityException(string.Format(_localizer["Phone number {0} is registered."], request.PhoneNumber));
                }
            }

            if (request.PhoneNumberConfirmed) user.PhoneNumberConfirmed = true;

            var userWithEmail = await _userManager.FindByEmailAsync(request.Email.Trim().Normalize());
            if (userWithEmail is null)
            {
                user.AddDomainEvent(new UserRegisteredEvent(user));
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesConstant.Operator);
                    if (!_mailSettings.EnableVerification)
                    {
                        return await Result<string>.SuccessAsync(user.Id, message: string.Format(_localizer["User {0} Registered."], user.UserName));
                    }

                    var messages = new List<string> { string.Format(_localizer["User {0} Registered."], user.UserName) };
                    if (_mailSettings.EnableVerification)
                    {
                        string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
                        TemplateMailRequest templateModel = new TemplateMailRequest()
                        {
                            Email = user.Email,
                            UserName = user.UserName.ToUpper(),
                            Url = HtmlEncoder.Default.Encode(emailVerificationUri),
                            Team = _templateMailSettings.TeamName,
                            TeamUrl = _templateMailSettings.TeamUrl,
                            Contact = _templateMailSettings.Contact,
                            SupportUrl = _templateMailSettings.SupportUrl,
                            SendBy = string.Format(_templateMailSettings.SendBy, user.Email)
                        };
                        var mailRequest = new MailRequest(
                            new List<string> { user.Email },
                            _localizer["Confirm Registration"],
                            await _templateService.GenerateEmailTemplate("email-confirmation", templateModel));
                        _jobService.Enqueue(() => _mailService.SendAsync(mailRequest));
                        messages.Add(_localizer[$"Please check your email account {user.Email} to verify."]);
                    }

                    _logger.LogInformation(string.Format(_localizer["Registered user:{0} successfull"], user.Id));
                    return await Result<string>.SuccessAsync(user.Id, messages: messages);
                }
                else
                {
                    throw new IdentityCustomException(_localizer, result.GetErrorMessages(_localizer));
                }
            }
            else
            {
                throw new IdentityException(string.Format(_localizer["Email {0} is registered."], request.Email));
            }
        }

        public async Task<IResult<string>> UpdateAsync(UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            _ = user ?? throw new UserNotFoundException(_localizer, request.Id);

            if (request.CurrentPassword != null && request.NewPassword != null)
            {
                await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            }

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.LastModifiedOn = DateTime.Now;
            user.LastModifiedBy = _currentUser.GetUserId().ToString();
            user.AddDomainEvent(new UserUpdatedEvent(user));
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation(string.Format(_localizer["Updated user:{0} successfull"], user.Id));
                return await Result<string>.SuccessAsync(user.Id, _localizer["User Updated Successfull."]);
            }
            else
            {
                await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                throw new IdentityException(_localizer["An error occurred while updating a user"], result.GetErrorMessages(_localizer));
            }
        }

        public async Task<IResult<string>> UpdatePictureAsync(string userId, UpdateUserPictureRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _ = user ?? throw new UserNotFoundException(_localizer, userId);
            string currentImageUrl = user.ImageUrl ?? string.Empty;
            if (request?.DeleteCurrentImageUrl == true)
            {
                await _uploadService.RemoveFileImage(UploadStorageType.Staff, currentImageUrl);
                user = user.ClearPathImageUrl();
                var fileUploadRequest = new FileUploadRequest
                {
                    Data = request?.Data,
                    Extension = Path.GetExtension(request.FileName).ToLower(),
                    UploadStorageType = UploadStorageType.Staff
                };
                string fileName = await GenerateFileName(20);
                fileUploadRequest.FileName = fileName + fileUploadRequest.Extension;
                user.ImageUrl = await _uploadService.UploadAsync(fileUploadRequest, FileType.Image);
            }

            string filePath = user.ImageUrl;
            user.AddDomainEvent(new UserPictureUpdateEvent(user));
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation(string.Format(_localizer["Updated picture user file:"], filePath));
                return await Result<string>.SuccessAsync(data: filePath);
            }
            else
            {
                await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                throw new IdentityException(_localizer["Update user picture failed"], result.GetErrorMessages(_localizer));
            }
        }

        public async Task<IResult<string>> UpdateRolesByUserAsync(string userId, UserRolesRequest request)
        {
            var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            _ = user ?? throw new UserNotFoundException(_localizer, userId);
            if (await _userManager.IsInRoleAsync(user, RolesConstant.Administrator))
            {
                return await Result<string>.FailAsync(_localizer["Not Allowed updated."]);
            }

            try
            {
                foreach (var userRole in request.UserRoles)
                {
                    if (await _roleManager.FindByNameAsync(userRole.RoleName) != null)
                    {
                        if (userRole.Selected)
                        {
                            if (!await _userManager.IsInRoleAsync(user, userRole.RoleName))
                            {
                                await _userManager.AddToRoleAsync(user, userRole.RoleName);
                            }
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(user, userRole.RoleName);
                        }
                    }
                }

                _logger.LogInformation(string.Format(_localizer["Updated roles user:{0}"], user.Id));
                return await Result<string>.SuccessAsync(userId, string.Format(_localizer["User Roles Updated Successfull."]));
            }
            catch (Exception)
            {
                throw new IdentityCustomException(_localizer, null);
            }
        }

        public async Task<IResult<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _ = user ?? throw new UserNotFoundException(_localizer, userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                _logger.LogInformation(string.Format(_localizer["Account Email {0} Confirmed"], user.Email));
                return await Result<string>.SuccessAsync(user.Id, string.Format(_localizer["Account Email {0} Confirmed. Use the /api/v1/identity/token endpoint to generate JWT."], user.Email));
            }
            else
            {
                throw new IdentityException(string.Format(_localizer["An error occurred while confirming {0}"], user.Email));
            }
        }

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            _ = user ?? throw new UserNotFoundException(_localizer, request.Email);
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new IdentityCustomException(_localizer, null);
            }

            string passwordResetUrl = await GeneratePasswordResetTokenAsync(user, origin);
            TemplateMailRequest templateModel = new TemplateMailRequest()
            {
                Email = user.Email,
                UserName = user.UserName.ToUpper(),
                Url = HtmlEncoder.Default.Encode(passwordResetUrl),
                Team = _templateMailSettings.TeamName,
                TeamUrl = _templateMailSettings.TeamUrl,
                Contact = _templateMailSettings.Contact,
                SupportUrl = _templateMailSettings.SupportUrl,
                SendBy = string.Format(_templateMailSettings.SendBy, user.Email)
            };
            var mailRequest = new MailRequest(
                new List<string> { user.Email },
                _localizer["Reset Password"],
                await _templateService.GenerateEmailTemplate("reset-password", templateModel));
            _jobService.Enqueue(() => _mailService.SendAsync(mailRequest));
            _logger.LogInformation(string.Format(_localizer["Password reset mail sent account email {0} to authorized."], user.Email));
            return await Result.SuccessAsync(_localizer["Password reset email has been sent."]);
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            _ = user ?? throw new UserNotFoundException(_localizer, request.Email);
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation(string.Format(_localizer["User {0} password reset"], user.Email));
                return await Result.SuccessAsync(_localizer["Password Reset Successful!"]);
            }
            else
            {
                await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                throw new IdentityException(_localizer["An error occurred while password reset"], result.GetErrorMessages(_localizer));
            }
        }

        public async Task<IResult> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _ = user ?? throw new UserNotFoundException(_localizer, userId);
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                TemplateMailRequest eMailModel = new TemplateMailRequest()
                {
                    Email = user.Email,
                    UserName = user.UserName.ToUpper(),
                    Team = _templateMailSettings.TeamName,
                    TeamUrl = _templateMailSettings.TeamUrl,
                    Contact = _templateMailSettings.Contact,
                    SupportUrl = _templateMailSettings.SupportUrl,
                    SendBy = string.Format(_templateMailSettings.SendBy, user.Email)
                };
                var mailRequest = new MailRequest(
                    new List<string> { user.Email },
                    _localizer["Change Password"],
                    await _templateService.GenerateEmailTemplate("change-password", eMailModel));
                _jobService.Enqueue(() => _mailService.SendAsync(mailRequest));
                _logger.LogInformation(string.Format(_localizer["User {0} changed password"], user.Email));
                return await Result<string>.SuccessAsync(_localizer["Change Password Successful!"]);
            }
            else
            {
                await Result<string>.FailAsync(result.GetErrorMessages(_localizer));
                throw new IdentityException(_localizer["An error occurred while change password"], result.GetErrorMessages(_localizer));
            }
        }

        public async Task<Result<string>> ExportAsync(string searchString = "")
        {
            var userFilterSpec = new UserFilterSpecification(searchString);
            var data = await _userManager.Users.AsNoTracking().AsQueryable().Specify(userFilterSpec).OrderByDescending(a => a.CreatedOn).ToListAsync();
            _ = data ?? throw new UserListEmptyException(_localizer);
            try
            {
                string result = await _excelService.ExportAsync(data, mappers: new Dictionary<string, Func<HureUser, object>>
                {
                    { _localizer["FirstName"], item => item.FirstName },
                    { _localizer["LastName"], item => item.LastName },
                    { _localizer["UserName"], item => item.UserName },
                    { _localizer["Email"], item => item.Email },
                    { _localizer["EmailConfirmed"], item => item.EmailConfirmed },
                    { _localizer["PhoneNumber"], item => item.PhoneNumber },
                    { _localizer["IsActive"], item => item.IsActive ? "Yes" : "No" },
                    { _localizer["CreatedOn"], item => item.CreatedOn.ToString("G", CultureInfo.CurrentCulture) }
                }, sheetName: _localizer["Users"]);
                _logger.LogInformation(_localizer["Exported Details User List To Excel"]);
                return await Result<string>.SuccessAsync(data: result);
            }
            catch (Exception)
            {
                throw new IdentityCustomException(_localizer, null);
            }
        }

        public async Task<Result<string>> DeleteAsync(string userId)
        {
            var user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            _ = user ?? throw new UserNotFoundException(_localizer, userId);
            if (await _userManager.IsInRoleAsync(user, RolesConstant.Administrator))
            {
                return await Result<string>.FailAsync(_localizer["Not allowed to delete"]);
            }

            user.AddDomainEvent(new UserDeletedEvent(user.Id));
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation(string.Format(_localizer["Deleted user:{0} successfull"], user.Id));
                return await Result<string>.SuccessAsync(user.Id, _localizer["User Deleted Successfull."]);
            }
            else
            {
                throw new IdentityException(_localizer["An error occurred while deleting a user"], result.GetErrorMessages(_localizer));
            }
        }

        private async Task<string> GetEmailVerificationUriAsync(HureUser user, string origin)
        {
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "api/v1/identity/confirm-email/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), StringKeys.UserId, user.Id);
            return QueryHelpers.AddQueryString(verificationUri, StringKeys.Code, code);
        }

        private async Task<string> GeneratePasswordResetTokenAsync(HureUser user, string origin)
        {
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            const string route = "api/v1/identity/reset-password/";
            var endpointUri = new Uri(string.Concat($"{origin}/", route));
            return QueryHelpers.AddQueryString(endpointUri.ToString(), StringKeys.Token, code);
        }

        private async Task<string> GenerateFileName(int length) =>
            await Utilities.GenerateCode("S", length);

        public async Task<bool> ExistsWithNameAsync(string name) =>
            await _userManager.FindByNameAsync(name) is not null;

        public async Task<bool> ExistsWithEmailAsync(string email, string exceptId = null) =>
            await _userManager.FindByEmailAsync(email.Trim().Normalize()) is HureUser user && user.Id != exceptId;

        public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string exceptId = null) =>
            await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is HureUser user && user.Id != exceptId;

        public async Task<int> GetCountAsync() =>
            await _userManager.Users.AsNoTracking().CountAsync();
    }
}
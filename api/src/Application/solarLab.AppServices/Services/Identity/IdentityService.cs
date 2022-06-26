using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using solarLab.AppServices.Services.Email;
using solarLab.Contracts.Enums;
using solarLab.Contracts.Identity;
using solarLab.Contracts.User;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Identity
{
    /// <inheritdoc/>
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public IdentityService(UserManager<ApplicationUser> userManager,
                                IMapper mapper, IConfiguration config,
                                RoleManager<ApplicationRole> roleManager,
                                IEmailService emailService,
                                IHttpContextAccessor httpContextAccessor, 
                                LinkGenerator linkGenerator)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
            _roleManager = roleManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        /// <inheritdoc/>
        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {

            var user = await _userManager.FindByEmailAsync(dto.Email);

            await CheckUser(user, dto.OldPassword);

            var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

            if (!result.Succeeded)
                throw new IdentityServiceException(string.Join('\n', result.Errors.Select(_ => _.Description)));

            await _emailService.SendEmailAsync(user.Email, "Изменение пароля", $"Ваш пароль был изменен {DateTime.Now}");

        }

        /// <inheritdoc/>
        public async Task ConfirmEmailAsync(string userId, string token)
        {
            if (userId == null || token == null)
            {
                throw new IdentityServiceException("Неверные данные");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new IdentityServiceException("Пользователь не найден");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
                throw new IdentityServiceException(string.Join('\n', result.Errors.Select(_=>_.Description)));

        }

        /// <inheritdoc/>
        public async Task<LoginResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);


            await CheckUser(user, dto.Password);

            return new LoginResponse
            {
                Token = await GenerateToken(user),
                User = _mapper.Map<UserDto>(user),              
            };
        }

        /// <inheritdoc/>
        public async Task<LoginResponse> ExternalLoginAsync(ExternalLoginDto dto)
        {
            var payload = await VerifyGoogleToken(dto);

            if (payload == null)
                throw new IdentityServiceException("Недействительный токен внешней аутентификации");

            var info = new UserLoginInfo(dto.Provider, payload.Subject, dto.Provider);

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new ApplicationUser { 
                        Email = payload.Email, 
                        UserName = payload.Email,
                        Name=payload.GivenName,
                        BirthDate=DateTime.Now, 
                        Gender=Gender.Undefined,
                        EmailConfirmed=payload.EmailVerified
                    };


                    using (var client = new WebClient())
                    {
                        user.Avatar = client.DownloadData(payload.Picture);
                    }

                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                        throw new IdentityServiceException(string.Join('\n', result.Errors.Select(_ => _.Description)));

                    await _userManager.AddLoginAsync(user, info);
                    if (!payload.EmailVerified)
                    {
                        await SendConfirmEmailLink(user);
                        throw new IdentityServiceException("Вы зарегистрированы. Подтвердите Email. Проверьте указанную почту");
                    }
                }
                else
                {
                    await _userManager.AddLoginAsync(user, info);
                }
            }

            if (user == null)
                throw new IdentityServiceException("Пользователь не найден");


            return new LoginResponse
            {
                Token = await GenerateToken(user),
                User = _mapper.Map<UserDto>(user),
            };
        }

        /// <inheritdoc/>
        public async Task SignupAsync(RegisterDto dto)
        {
            var user = _mapper.Map<ApplicationUser>(dto);
            if (user.Avatar.Length < 1)
            {
                user.Avatar = System.IO.File.ReadAllBytes("wwwroot/images/avatar/defaultAvatar.jpg");
            }
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new IdentityServiceException(string.Join('\n', result.Errors.Select(_ => _.Description)));


            await SendConfirmEmailLink(user);
        }



        private async Task SendConfirmEmailLink(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = _linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext,
                        action: "ConfirmEmail",         
                        controller: "Identity",
                        values: new { userId = user.Id, token = token },
                        scheme:_httpContextAccessor.HttpContext.Request.Scheme);


            await _emailService.SendEmailAsync(user.Email, "Подтверждение аккаунта",
                $"Подтвердите регистрацию, перейдя по <a href='{callbackUrl}'>ссылке</a>.");
        }

        private async Task CheckUser(ApplicationUser user, string password)
        {
            if (user == null)
            {
                throw new IdentityServiceException("Пользователь не найден");
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                throw new IdentityServiceException("Неверный пароль");
            }

            if (!user.EmailConfirmed)
            {
                await SendConfirmEmailLink(user);
                throw new IdentityServiceException("Вы зарегистрированы. Подтвердите Email. Проверьте указанную почту");
            }
        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginDto externalAuth)
        {

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _config["Authentication:Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken, settings);
            return payload;
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };


            user.Roles = await _userManager.GetRolesAsync(user);

            if (user.Roles.Count == 0)
            {
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                user.Roles = await _userManager.GetRolesAsync(user);
            }

            claims.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Secret:key"]));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        /// <inheritdoc/>
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new IdentityServiceException("Пользователь не найден");

            if(await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new IdentityServiceException("Текущий и новый пароли не могут быть одинаковыми");



            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);

            if (!result.Succeeded)
                throw new IdentityServiceException(string.Join('\n', result.Errors.Select(_ => _.Description)));

            await _emailService.SendEmailAsync(user.Email, "Изменение пароля", $"Ваш пароль был изменен {DateTime.Now}");


        }

        /// <inheritdoc/>
        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new IdentityServiceException("Пользователь не найден");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", dto.Email }
            };


            var callbackUrl = QueryHelpers.AddQueryString(dto.ClientURI, param);

            await _emailService.SendEmailAsync(dto.Email, "Сброс пароля", $"Для сброса пароля перейдите по <a href='{callbackUrl}'>ссылке</a>.");
        }
    }
}

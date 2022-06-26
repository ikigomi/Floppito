using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.Identity;
using solarLab.AppServices.Services.User;
using solarLab.Contracts.Enums;
using solarLab.Contracts.Identity;
using solarLab.Contracts.User;
using solarLab.Infrastructure.Exceptions;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для авторизации/регистрации
    /// </summary>
    [AllowAnonymous]
    public class IdentityController : ApiController
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService, ILogger<IdentityController> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        /// <summary>
        /// Вход по логину \ паролю
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = await _identityService.LoginAsync(dto);

            return Ok(response);

        }
        /// <summary>
        /// Вход через сторонний сервис
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(ExternalLogin))]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto dto)
        {
            var response = await _identityService.ExternalLoginAsync(dto);

            return Ok(response);

        }
        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await _identityService.ResetPasswordAsync(dto);

            return Ok();

        }
        /// <summary>
        /// Восстановление пароля
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(ForgotPassword))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _identityService.ForgotPasswordAsync(dto);

            return Ok();

        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Signup))]
        public async Task<IActionResult> Signup([FromBody] RegisterDto dto)
        {
            var a = dto.Avatar.Length;
            await _identityService.SignupAsync(dto);

            return Ok();
        }

        /// <summary>
        /// Подтверждение email адреса
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            await _identityService.ConfirmEmailAsync(userId, token);

            return Content("Email адрес подтвержден");
        }

        /// <summary>
        /// Изменение пароля
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            await _identityService.ChangePasswordAsync(dto);

            return Ok();
        }

    }
}

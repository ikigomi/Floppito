using solarLab.Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Identity
{
    /// <summary>
    /// Сервис для работы с identity
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<LoginResponse> LoginAsync(LoginDto dto);
        /// <summary>
        /// Авторизация через сторонние сервисы
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<LoginResponse> ExternalLoginAsync(ExternalLoginDto dto);
        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task ResetPasswordAsync(ResetPasswordDto dto);
        /// <summary>
        /// Восстановление пароля
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task ForgotPasswordAsync(ForgotPasswordDto dto);

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task SignupAsync(RegisterDto dto);

        /// <summary>
        /// Подтверждение почты
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ConfirmEmailAsync(string userId, string token);
        /// <summary>
        /// Изменение пароля
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(ChangePasswordDto dto);
    }
}

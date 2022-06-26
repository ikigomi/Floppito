using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Identity
{
    /// <summary>
    /// Модель для сброса пароля
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Identity
{
    /// <summary>
    /// Модель изменения пароля
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Старый пароль
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }
        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        public string ConfirmNewPassword { get; set; }
    }
}

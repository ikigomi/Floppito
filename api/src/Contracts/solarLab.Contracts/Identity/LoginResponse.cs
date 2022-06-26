using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Identity
{
    /// <summary>
    /// Модель ответа при успешном входе
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserDto User { get; set; }
    }
}

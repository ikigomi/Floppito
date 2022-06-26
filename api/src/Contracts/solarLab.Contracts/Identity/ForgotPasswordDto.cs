using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Identity
{
    /// <summary>
    /// Модель для восстановление пароля
    /// </summary>
    public class ForgotPasswordDto
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Адрес для callback 
        /// </summary>
        public string ClientURI { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Identity
{
    /// <summary>
    /// Модель для входа через сторонний сервис
    /// </summary>
    public class ExternalLoginDto
    {
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// Токен
        /// </summary>
        public string IdToken { get; set; }
    }
}

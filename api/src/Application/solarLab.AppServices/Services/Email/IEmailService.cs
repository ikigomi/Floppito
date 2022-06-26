using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Email
{
    /// <summary>
    /// Сервис для работы с email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправка email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}

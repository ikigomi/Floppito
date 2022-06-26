using solarLab.Contracts.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Message
{
    /// <summary>
    /// Сервис для работы с сообщениями
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Удаляет запись.
        /// </summary>
        /// <returns></returns>
        Task DeleteMessageAsync(Guid id);

        /// <summary>
        /// Обновляет записи.
        /// </summary>
        /// <returns></returns>
        Task UpdateMessageAsync(MessageDto model);

        /// <summary>
        /// Добавляет записи.
        /// </summary>
        /// <returns></returns>
        Task<MessageDto> AddMessageAsync(MessageDto model);
    }
}

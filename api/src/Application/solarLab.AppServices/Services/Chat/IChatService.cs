using solarLab.Contracts.Chat;
using solarLab.Contracts.Message;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Chat
{
    /// <summary>
    /// Сервис для работы с чатами
    /// </summary>
    public interface IChatService
    {
        /// <summary>
        /// Возвращает все записи
        /// </summary>
        /// <returns></returns>
        Task<List<ChatDto>> GetChatsByUserAsync();

        /// <summary>
        /// Возвращает запись по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ChatDto> GetChatByIdAsync(Guid id);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ChatDto> AddChatAsync(ChatDto model);

        /// <summary>
        /// Возвращает идентификатор чата, в котором состоят пользователи
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Guid GetChatIdByUsers(IEnumerable<Guid> userIds);

        /// <summary>
        /// Добавляет пользователя в чат
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AddUserToChatAsync(Guid chatId, Guid userId);
    }
}

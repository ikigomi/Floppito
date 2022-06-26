using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.User
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Добавить администраторов
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task AddAdminsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Удалить администраторов
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteAdminsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Возвращает пользователя по айди
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto> GetUserAsync(Guid id);

        /// <summary>
        /// Возвращает текущего пользователя
        /// </summary>
        /// <returns></returns>
        Task<UserDto> GetCurrentUserAsync();

        /// <summary>
        /// Возвращает всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<List<UserDto>> GetUsers();
    }
}

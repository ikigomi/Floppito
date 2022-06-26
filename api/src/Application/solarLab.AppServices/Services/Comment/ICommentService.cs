using solarLab.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Comment
{
    /// <summary>
    /// Сервис для работы с комментариями
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        /// <returns></returns>
        Task<List<CommentDto>> GetCommentsAsync();

        /// <summary>
        /// Возвращает все записи по идентификатору поста.
        /// </summary>
        /// <returns></returns>
        Task<List<CommentDto>> GetCommentsByPostAsync(Guid postId);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommentDto> AddCommentAsync(CommentDto model);

        /// <summary>
        /// Обновляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateCommentAsync(CommentDto model);

        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCommentAsync(Guid id);

        /// <summary>
        /// Удаляет несколько записей
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteCommentsAsync(IEnumerable<Guid> ids);

    }
}

using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Comment
{
    public interface ICommentRepository:IRepository<CommentEntity>
    {
        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        /// <returns></returns>
        Task<List<CommentEntity>> GetCommentsAsync();
        Task <CommentEntity> GetCommentByIdAsync(Guid commentId);

        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        /// <returns></returns>
        Task<List<CommentEntity>> GetCommentsByPostAsync(Guid postId);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task<CommentEntity> AddCommentAsync(CommentEntity comment);

        /// <summary>
        /// Обновляет запись
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task UpdateCommentAsync(CommentEntity comment);

        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCommentAsync(Guid id);

        Task DeleteCommentsAsync(IEnumerable<Guid> ids);

    }
}

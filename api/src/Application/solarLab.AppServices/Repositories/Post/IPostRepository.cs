using solarLab.Contracts.Search;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Post
{
    /// <summary>
    /// Репозиторий для работы с объявлениями
    /// </summary>
    public interface IPostRepository : IRepository<PostEntity>
    {
        /// <summary>
        /// Возвращает все записи.
        /// </summary>
        /// <returns></returns>
        Task<List<PostEntity>> GetAcceptedPostsAsync();
        Task<List<PostEntity>> GetNewPostsAsync();
        Task<List<PostEntity>> GetRejectedPostsAsync();

        /// <summary>
        /// Возвращает указанную запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostEntity> GetPostAsync(Guid id);
        Task<PostEntity> GetPostWithCommentsAsync(Guid id);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task<PostEntity> AddPostAsync(PostEntity post);

        /// <summary>
        /// Обновляет запись
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        Task UpdatePostAsync(PostEntity post);
        Task UpdatePostsAsync(IEnumerable<PostEntity> posts);


        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePostAsync(Guid id);

        Task DeletePostsAsync(IEnumerable<Guid> ids);

        Task<List<PostEntity>> GetAcceptedPostsBySearchAsync(string searchString);
        Task<List<PostEntity>> GetAcceptedPostsBySearchAsync(FilterDto filter);
        Task<List<PostEntity>> GetAcceptedPostsByCategoryAsync(Guid categoryId);
        Task<List<PostEntity>> GetAcceptedPostsByUserAsync(Guid userId);


    }
}

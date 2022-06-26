using solarLab.Contracts.Post;
using solarLab.Contracts.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Post
{
    /// <summary>
    /// Сервис для работы с объявлениями
    /// </summary>
    public interface IPostService
    {
        /// <summary>
        /// Возвращает все одобренные записи.
        /// </summary>
        /// <returns></returns>
        Task<List<PostDto>> GetAcceptedPostsAsync();

        /// <summary>
        /// Возвращает все новые записи.
        /// </summary>
        /// <returns></returns>
        Task<List<PostDto>> GetNewPostsAsync();

        /// <summary>
        /// Возвращает все отклоненные записи.
        /// </summary>
        /// <returns></returns>
        Task<List<PostDto>> GetRejectedPostsAsync();

        /// <summary>
        /// Возвращает указанную запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostDto> GetPostAsync(Guid id);

        /// <summary>
        /// Возвращает указанную запись с комментариями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostDto> GetPostWithCommentsAsync(Guid id);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PostDto> AddPostAsync(PostDto model);

        /// <summary>
        /// Обновляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdatePostAsync(PostDto model);

        /// <summary>
        /// Обновляет несколько записей
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task UpdatePostsAsync(IEnumerable<PostDto> models);

        /// <summary>
        /// Одобряет посты
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task AcceptPostsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Отклоняет посты
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task RejectPostsAsync(IEnumerable<Guid> ids);


        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeletePostAsync(Guid id);

        /// <summary>
        /// Удаляет несколькой записей
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeletePostsAsync(IEnumerable<Guid> ids);

        /// <summary>
        /// Возвращает все одобренные записи по строке поиска.
        /// </summary>
        /// <returns></returns>
        Task<List<PostDto>> GetAcceptedPostsBySearchAsync(string searchString);

        Task<List<PostDto>> GetAcceptedPostsBySearchAsync(FilterDto filter);

        /// <summary>
        /// Возвращает все одобренные записи по категории.
        /// </summary>
        /// <returns></returns>
        Task<List<PostDto>> GetAcceptedPostsByCategoryAsync(Guid categoryId);

        /// <summary>
        /// Возвращает все одобренные записи по идентификатору пользователя.
        /// </summary>
        /// <returns></returns>
        Task<List<PostDto>> GetAcceptedPostsByUserAsync(Guid userId);

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.Post;
using solarLab.Contracts.Enums;
using solarLab.Contracts.Post;
using solarLab.Contracts.Search;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для постов
    /// </summary>
    public class PostController : ApiController
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        /// <summary>
        /// Получение списка одобренных постов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _postService.GetAcceptedPostsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получение списка постов по строке фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(GetPostsBySearch))]
        public async Task<IActionResult> GetPostsBySearch([FromQuery] FilterDto filter)
        {
            var result = await _postService.GetAcceptedPostsBySearchAsync(filter);
            return Ok(result);
        }

        /// <summary>
        /// Получение списка постов по категории
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(GetPostsByCategory))]
        public async Task<IActionResult> GetPostsByCategory([FromQuery] Guid categoryId)
        {
            var result = await _postService.GetAcceptedPostsByCategoryAsync(categoryId);
            return Ok(result);
        }

        /// <summary>
        /// Получение списка постов по пользователю
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(GetPostsByUser))]
        public async Task<IActionResult> GetPostsByUser([FromQuery] Guid userId)
        {
            var result = await _postService.GetAcceptedPostsByUserAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Получение новых постов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        [Route(nameof(GetNewPosts))]       
        public async Task<IActionResult> GetNewPosts()
        {
            var result = await _postService.GetNewPostsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получение списка отвергнутых постов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        [Route(nameof(GetRejectedPosts))]
        public async Task<IActionResult> GetRejectedPosts()
        {
            var result = await _postService.GetRejectedPostsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получение поста по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _postService.GetPostAsync(id);
            return Ok(result);
        }
        /// <summary>
        /// Добавление поста
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PostDto model)
        {
            var post = await _postService.AddPostAsync(model);
            return Created(string.Empty, post);
        }

        /// <summary>
        /// Обновление поста
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PostDto model)
        {
            await _postService.UpdatePostAsync(model);
            return Ok();
        }

        /// <summary>
        /// Удаление поста
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _postService.DeletePostAsync(id);
            return Ok();
        }
        /// <summary>
        /// Удаление постов
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route(nameof(DeleteRange))]
        [HttpPost]
        public async Task<IActionResult> DeleteRange([FromBody] Guid[] ids)
        {
            await _postService.DeletePostsAsync(ids);
            return Ok();
        }

        /// <summary>
        /// Получение поста с комментариями
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetPostWithComments) + "/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPostWithComments([FromRoute] Guid id)
        {
            var result = await _postService.GetPostWithCommentsAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Одобрить указанные посты
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(AcceptPosts))]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> AcceptPosts([FromBody] Guid[] ids)
        {
            await _postService.AcceptPostsAsync(ids);
            return Ok();
        }

        /// <summary>
        /// Отклонить указанные посты
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(RejectPosts))]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> RejectPosts([FromBody] Guid[] ids)
        {
            await _postService.RejectPostsAsync(ids);
            return Ok();
        }
    }
}

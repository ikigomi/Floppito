using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.Comment;
using solarLab.Contracts.Comment;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с комментариями
    /// </summary>
    public class CommentController : ApiController
    {
        private readonly ILogger<CommentController> _logger;
        private readonly ICommentService _commentService;

        public CommentController(ILogger<CommentController> logger, ICommentService postService)
        {
            _logger = logger;
            _commentService = postService;
        }

        /// <summary>
        /// Получение списка комментариев
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _commentService.GetCommentsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CommentDto model)
        {
            var comment = await _commentService.AddCommentAsync(model);
            return Created(string.Empty, comment);
        }

        /// <summary>
        /// Обновление комментария
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CommentDto model)
        {
            await _commentService.UpdateCommentAsync(model);
            return Ok();
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _commentService.DeleteCommentAsync(id);
            return Ok();
        }

        /// <summary>
        /// Удаление комментариев
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route(nameof(DeleteRange))]
        [HttpPost]
        public async Task<IActionResult> DeleteRange([FromBody] Guid[] ids)
        {
            await _commentService.DeleteCommentsAsync(ids);
            return Ok();
        }

    }
}

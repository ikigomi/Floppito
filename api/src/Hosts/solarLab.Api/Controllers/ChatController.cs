using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.Chat;
using solarLab.Contracts.Chat;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с чатами
    /// </summary>
    public class ChatController : ApiController
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chatService;

        public ChatController(ILogger<ChatController> logger, IChatService chatService)
        {
            _logger = logger;
            _chatService = chatService;
        }

        /// <summary>
        /// Получение чата по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var result = await _chatService.GetChatByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получение чатов для текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetByUser) + "/{userId:guid}")]
        public async Task<IActionResult> GetByUser()
        {
            var result = await _chatService.GetChatsByUserAsync();
            return Ok(result);
        }

        /// <summary>
        /// Добавление чата
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ChatDto model)
        {
            var chat = await _chatService.AddChatAsync(model);
            return Created(string.Empty, chat);
        }

        /// <summary>
        /// Получение чата для указанных пользователей
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetIdByUsers))]
        public IActionResult GetIdByUsers([FromQuery] Guid[] userIds)
        {
            var result = _chatService.GetChatIdByUsers(userIds);
            return Ok(result);
        }
    }
}

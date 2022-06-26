using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.Message;
using solarLab.Contracts.Message;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для сообщений
    /// </summary>
    public class MessageController : ApiController
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageService _messageService;

        public MessageController(ILogger<MessageController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        /// <summary>
        /// Обновить сообщение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MessageDto model)
        {
            await _messageService.UpdateMessageAsync(model);
            return Ok();
        }

        /// <summary>
        /// Добавить сообщение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageDto model)
        {
            var result = await _messageService.AddMessageAsync(model);
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.User;
using solarLab.Contracts.Enums;
using solarLab.Contracts.User;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для пользователей
    /// </summary>
    public class UserController : ApiController
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetUser) + "/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var result = await _userService.GetUserAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetUser))]
        public async Task<IActionResult> GetUser()
        {
            var result = await _userService.GetCurrentUserAsync();
            return Ok(result);
        }

        /// <summary>
        /// Добавление пользователям роли администратора
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(AddAdmins))]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> AddAdmins([FromBody] Guid[] ids)
        {
            await _userService.AddAdminsAsync(ids);
            return Ok();
        }

        /// <summary>
        /// Удаление у пользователей роли администратора
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(DeleteAdmins))]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> DeleteAdmins([FromBody] Guid[] ids)
        {
            await _userService.DeleteAdminsAsync(ids);
            return Ok();
        }
    }
}

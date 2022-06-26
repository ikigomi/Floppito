using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarLab.AppServices.Services.Category;
using solarLab.Contracts.Category;
using solarLab.Contracts.Enums;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для категорий
    /// </summary>
    public class CategoryController : ApiController
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService postService)
        {
            _logger = logger;
            _categoryService = postService;
        }

        /// <summary>
        /// Получение списка категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetCategoriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Добавление категории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Add([FromBody] CategoryDto model)
        {
            var category = await _categoryService.AddCategoryAsync(model);
            return Created(string.Empty, category);
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Update([FromBody] CategoryDto model)
        {
            await _categoryService.UpdateCategoryAsync(model);
            return Ok();
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        [HttpDelete]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }

        /// <summary>
        /// Удаление категорий
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [Route(nameof(DeleteRange))]
        [HttpPost]
        public async Task<IActionResult> DeleteRange([FromBody] Guid[] ids)
        {
            await _categoryService.DeleteCategoriesAsync(ids);
            return Ok();
        }
    }
}

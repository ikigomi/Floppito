using solarLab.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Category
{
    /// <summary>
    /// Сервис для работы с категориями
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает все категории
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryDto>> GetCategoriesAsync();

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CategoryDto> AddCategoryAsync(CategoryDto model);

        /// <summary>
        /// Обновляет запись
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task UpdateCategoryAsync(CategoryDto model);

        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCategoryAsync(Guid id);

        /// <summary>
        /// Удаляет записи
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteCategoriesAsync(IEnumerable<Guid> ids);

    }
}

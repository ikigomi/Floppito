using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Category
{
    public interface ICategoryRepository:IRepository<CategoryEntity>
    {
        Task<List<CategoryEntity>> GetCategoriesAsync();

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<CategoryEntity> AddCategoryAsync(CategoryEntity category);

        /// <summary>
        /// Обновляет запись
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task UpdateCategoryAsync(CategoryEntity category);

        /// <summary>
        /// Удаляет запись
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteCategoryAsync(Guid id);

        Task DeleteCategoriesAsync(IEnumerable<Guid> ids);
    }
}

using Microsoft.EntityFrameworkCore;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Category
{
    public class CategoryRepository:Repository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<CategoryEntity> AddCategoryAsync(CategoryEntity category)
        {
            return await AddAsync(category);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await GetByIdAsync(id);
            await DeleteAsync(category);
        }

        public async Task DeleteCategoriesAsync(IEnumerable<Guid> ids)
        {
            var categories = await GetAllFiltered(c => ids.Contains(c.Id)).ToListAsync();
            await DeleteAsync(categories);
        }

        public async Task<List<CategoryEntity>> GetCategoriesAsync()
        {
            return await GetAll()
                .ToListAsync();
        }

        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            await UpdateAsync(category);
        }
    }
}

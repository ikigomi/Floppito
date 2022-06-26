using AutoMapper;
using Microsoft.EntityFrameworkCore;
using solarLab.AppServices.Repositories.Category;
using solarLab.Contracts.Category;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Category
{
    /// <inheritdoc/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        /// <inheritdoc/>
        public async Task<CategoryDto> AddCategoryAsync(CategoryDto model)
        {
            var category = _mapper.Map<CategoryEntity>(model);
            var result = await _categoryRepository.AddCategoryAsync(category);
            return _mapper.Map<CategoryDto>(result);
        }

        /// <inheritdoc/>
        public async Task DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        /// <inheritdoc/>
        public async Task DeleteCategoriesAsync(IEnumerable<Guid> ids)
        {
            await _categoryRepository.DeleteCategoriesAsync(ids);
        }

        /// <inheritdoc/>
        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var result = await _categoryRepository.GetCategoriesAsync();
            return result.Count > 0 ? _mapper.Map<List<CategoryDto>>(result) : new List<CategoryDto>();

        }

        /// <inheritdoc/>
        public async Task UpdateCategoryAsync(CategoryDto model)
        {
            var category = _mapper.Map<CategoryEntity>(model);
            await _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}

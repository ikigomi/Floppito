using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using solarLab.AppServices.Repositories.Category;
using solarLab.AppServices.Services.Category;
using solarLab.Contracts.Category;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using solarLab.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace solarLab.Tests.Category
{
    public class CategoryServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Fixture _fixture;

        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            var config = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            _mapper = config.CreateMapper();
            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mapper, _categoryRepository.Object);
            _fixture = new Fixture();

        }

        [Fact]
        public async Task GetCategoriesAsync_Should_Return_Categories()
        {
            //Arrange
            var categories = _fixture
                               .Build<CategoryEntity>()
                               .Without(_ => _.Posts)
                               .CreateMany()
                               .ToList();

            _categoryRepository
                .Setup(_ => _.GetCategoriesAsync())
                .ReturnsAsync(categories)
                .Verifiable();

            //Act
            var result = await _categoryService.GetCategoriesAsync();

            //Assert
            _categoryRepository.Verify();
            _categoryRepository.Verify(_ => _.GetCategoriesAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [AutoData]
        public async Task AddCategoryAsync_Should_Return_Category(CategoryDto model)
        {
            var category = _fixture
                              .Build<CategoryEntity>()
                              .Without(_ => _.Posts)
                              .Create();

            _categoryRepository
                .Setup(_ => _.AddCategoryAsync(It.IsAny<CategoryEntity>()))
                .ReturnsAsync(category)
                .Verifiable();

            var result = await _categoryService.AddCategoryAsync(model);

            _categoryRepository.Verify();
            _categoryRepository.Verify(_ => _.AddCategoryAsync(It.IsAny<CategoryEntity>()), Times.Once);
            Assert.NotNull(result);
        }

        [Theory]
        [AutoData]
        public async Task UpdateCategoryAsync_Should_Call_UpdateCategoryAsync(CategoryDto model)
        {
            _categoryRepository
                .Setup(_ => _.UpdateCategoryAsync(It.IsAny<CategoryEntity>()))
                .Verifiable();

            await _categoryService.UpdateCategoryAsync(model);

            _categoryRepository.Verify();
            _categoryRepository.Verify(_ => _.UpdateCategoryAsync(It.IsAny<CategoryEntity>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task DeleteCategoryAsync_Should_Call_DeleteCategoryAsync(Guid id)
        {
            _categoryRepository
                .Setup(_ => _.DeleteCategoryAsync(It.IsAny<Guid>()))
                .Verifiable();

            await _categoryService.DeleteCategoryAsync(id);

            _categoryRepository.Verify();
            _categoryRepository.Verify(_ => _.DeleteCategoryAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task DeleteCategoriesAsync_Should_Call_DeleteCategoriesAsync(IEnumerable<Guid> ids)
        {
            _categoryRepository
                .Setup(_ => _.DeleteCategoriesAsync(It.IsAny<IEnumerable<Guid>>()))
                .Verifiable();

            await _categoryService.DeleteCategoriesAsync(ids);

            _categoryRepository.Verify();
            _categoryRepository.Verify(_ => _.DeleteCategoriesAsync(It.IsAny<IEnumerable<Guid>>()), Times.Once);
        }
    }
}

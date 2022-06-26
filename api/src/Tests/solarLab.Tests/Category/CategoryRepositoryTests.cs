using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Moq;
using solarLab.AppServices.Repositories.Category;
using solarLab.DataAccess;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using solarLab.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace solarLab.Tests.Category
{
    public class CategoryRepositoryTests //TODO
    {
        //private readonly Mock<IRepository<CategoryEntity>> _repository;
        //private readonly Fixture _fixture;

        //private readonly CategoryRepository _categoryRepository;

        //public CategoryRepositoryTests()
        //{
        //    _repository = new Mock<IRepository<CategoryEntity>>();
        //    var mockContext = new Mock<MigrationDbContext>();
        //    _categoryRepository = new CategoryRepository(mockContext.Object);
        //    _fixture = new Fixture();

        //}

        //[Fact]
        //public async Task GetCategoriesAsync_Should_Return_Categories()
        //{
        //    //Arrange
        //    var categories = _fixture
        //                       .Build<CategoryEntity>()
        //                       .Without(_ => _.Posts)
        //                       .CreateMany()
        //                       .ToList();

        //    _repository
        //        .Setup(_ => _.GetAll().ToListAsync(CancellationToken.None))
        //        .ReturnsAsync(categories)
        //        .Verifiable();

        //    //Act
        //    var result = await _categoryRepository.GetCategoriesAsync();

        //    //Assert
        //    _repository.Verify();
        //    _repository.Verify(_ => _.GetAll(), Times.Once);

        //    Assert.NotNull(result);
        //    Assert.NotEmpty(result);
        //}

        //[Theory]
        //[AutoData]
        //public async Task AddCategoryAsync_Should_Return_Category(CategoryEntity model)
        //{
        //    var category = _fixture
        //                      .Build<CategoryEntity>()
        //                      .Without(_ => _.Posts)
        //                      .Create();

        //    _repository
        //        .Setup(_ => _.AddAsync(It.IsAny<CategoryEntity>()))
        //        .ReturnsAsync(category)
        //        .Verifiable();

        //    var result = await _categoryRepository.AddCategoryAsync(model);

        //    _repository.Verify();
        //    _repository.Verify(_ => _.AddAsync(It.IsAny<CategoryEntity>()), Times.Once);
        //    Assert.NotNull(result);
        //}

        //[Theory]
        //[AutoData]
        //public async Task UpdateCategoryAsync_Should_Call_UpdateAsync(CategoryEntity category)
        //{
        //    _repository
        //        .Setup(_ => _.UpdateAsync(It.IsAny<CategoryEntity>()))
        //        .Verifiable();

        //    await _categoryRepository.UpdateCategoryAsync(category);

        //    _repository.Verify();
        //    _repository.Verify(_ => _.UpdateAsync(It.IsAny<CategoryEntity>()), Times.Once);
        //}

        //[Theory]
        //[AutoData]
        //public async Task DeleteCategoryAsync_Should_Call_DeleteAsync(Guid id)
        //{

        //    _repository
        //        .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
        //        .Verifiable();

        //    _repository
        //        .Setup(_ => _.DeleteAsync(It.IsAny<CategoryEntity>()))
        //        .Verifiable();

        //    await _categoryRepository.DeleteCategoryAsync(id);

        //    _repository.Verify();
        //    _repository.Verify(_ => _.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        //    _repository.Verify(_ => _.DeleteAsync(It.IsAny<CategoryEntity>()), Times.Once);
        //}

        //[Theory]
        //[AutoData]
        //public async Task DeleteCategoriesAsync_Should_Call_DeleteAsync(IEnumerable<Guid> ids)
        //{
        //    _repository
        //        .Setup(_ => _.GetAllFiltered(It.IsAny<Expression<Func<CategoryEntity, bool>>>()))
        //        .Verifiable();

        //    _repository
        //        .Setup(_ => _.DeleteAsync(It.IsAny<IEnumerable<CategoryEntity>>()))
        //        .Verifiable();

        //    await _categoryRepository.DeleteCategoriesAsync(ids);

        //    _repository.Verify();
        //    _repository.Verify(_ => _.GetAllFiltered(It.IsAny<Expression<Func<CategoryEntity, bool>>>()), Times.Once);
        //    _repository.Verify(_ => _.DeleteAsync(It.IsAny<IEnumerable<CategoryEntity>>()), Times.Once);
        //}

    }
}

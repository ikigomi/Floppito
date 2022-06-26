using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using solarLab.Api.Controllers;
using solarLab.AppServices.Services.Post;
using solarLab.Contracts.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace solarLab.Tests.Post
{
    public class PostControllerTests
    {
        private readonly Mock<IPostService> _postService;
        private readonly Fixture _fixture;

        private readonly PostController _postController;

        public PostControllerTests()
        {
            _postService = new Mock<IPostService>();
            _fixture = new Fixture();
            _postController = new PostController(null, _postService.Object);

        }

        [Fact]
        public async Task Get_Should_Return_IActionResult()
        {
            //Arrange
            List<PostDto> posts = _fixture
                               .Build<PostDto>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.Author)
                               .CreateMany()
                               .ToList();

            _postService
                .Setup(_ => _.GetAcceptedPostsAsync())
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            await _postController.Get();

            //Assert
            _postService.Verify();
            _postService.Verify(_ => _.GetAcceptedPostsAsync(), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task GetPostsBySearch_Should_Return_IActionResult(string searchString)
        {
            //Arrange
            List<PostDto> posts = _fixture
                               .Build<PostDto>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.Author)
                               .CreateMany()
                               .ToList();

            _postService
                .Setup(_ => _.GetAcceptedPostsBySearchAsync(It.IsAny<string>()))
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            await _postController.GetPostsBySearch(searchString);

            //Assert
            _postService.Verify();
            _postService.Verify(_ => _.GetAcceptedPostsBySearchAsync(It.IsAny<string>()), Times.Once);
        }

    }
}

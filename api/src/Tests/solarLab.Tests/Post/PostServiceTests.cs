using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using solarLab.AppServices.Repositories.Post;
using solarLab.AppServices.Services.Post;
using solarLab.Contracts.Enums;
using solarLab.Contracts.Post;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using solarLab.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace solarLab.Tests.Post
{
    public class PostServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Fixture _fixture;

        private readonly PostService _postService;

        public PostServiceTests()
        {
            var config = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            _mapper = config.CreateMapper();
            _postRepository = new Mock<IPostRepository>();
            _postService = new PostService(_postRepository.Object, _mapper);
            _fixture = new Fixture();

        }

        [Fact]
        public async Task GetAcceptedPostsAsync_Should_Return_AcceptedPosts()
        {
            //Arrange
            List<PostEntity> posts = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .With(x => x.PostStatusId, PostStatus.Accepted)
                               .CreateMany()
                               .ToList();

            _postRepository
                .Setup(_ => _.GetAcceptedPostsAsync())
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            List<PostDto> result = await _postService.GetAcceptedPostsAsync();

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetAcceptedPostsAsync(), Times.Once);

            Assert.All(posts, x => Assert.Equal(PostStatus.Accepted, x.PostStatusId));
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetNewPostsAsync_Should_Return_NewPosts()
        {
            //Arrange
            List<PostEntity> posts = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .With(x => x.PostStatusId, PostStatus.New)
                               .CreateMany()
                               .ToList();

            _postRepository
                .Setup(_ => _.GetNewPostsAsync())
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            List<PostDto> result = await _postService.GetNewPostsAsync();

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetNewPostsAsync(), Times.Once);

            Assert.All(posts, x => Assert.Equal(PostStatus.New, x.PostStatusId));

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetRejectedPostsAsync_Should_Return_RejectedPosts()
        {
            //Arrange
            List<PostEntity> posts = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .With(x => x.PostStatusId, PostStatus.Rejected)
                               .CreateMany()
                               .ToList();

            _postRepository
                .Setup(_ => _.GetRejectedPostsAsync())
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            List<PostDto> result = await _postService.GetRejectedPostsAsync();

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetRejectedPostsAsync(), Times.Once);

            Assert.All(posts, x => Assert.Equal(PostStatus.Rejected, x.PostStatusId));
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [AutoData]
        public async Task GetPostAsync_Should_Return_Post(Guid id)
        {
            //Arrange
            var post = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .Create();

            _postRepository
                .Setup(_ => _.GetPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(post)
                .Verifiable();

            //Act
            var result = await _postService.GetPostAsync(id);

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetPostAsync(It.IsAny<Guid>()), Times.Once);

            Assert.NotNull(result);
        }

        [Theory]
        [AutoData]
        public async Task AddPostAsync_Should_Return_Post(PostDto model)
        {
            var post = _fixture
                              .Build<PostEntity>()
                              .Without(_ => _.Comments)
                              .Without(_ => _.User)
                              .Without(_ => _.Category)
                              .Create();

            _postRepository
                .Setup(_ => _.AddPostAsync(It.IsAny<PostEntity>()))
                .ReturnsAsync(post)
                .Verifiable();

            var result = await _postService.AddPostAsync(model);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.AddPostAsync(It.IsAny<PostEntity>()), Times.Once);
            Assert.NotNull(result);
        }

        [Theory]
        [AutoData]
        public async Task UpdatePostAsync_Should_Call_UpdatePostAsync(PostDto model)
        {
            _postRepository
                .Setup(_ => _.UpdatePostAsync(It.IsAny<PostEntity>()))
                .Verifiable();

            await _postService.UpdatePostAsync(model);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.UpdatePostAsync(It.IsAny<PostEntity>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task UpdatePostsAsync_Should_Call_UpdatePostsAsync(IEnumerable<PostDto> models)
        {
            _postRepository
                .Setup(_ => _.UpdatePostsAsync(It.IsAny<List<PostEntity>>()))
                .Verifiable();

            await _postService.UpdatePostsAsync(models);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.UpdatePostsAsync(It.IsAny<List<PostEntity>>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task AcceptPostsAsync_Should_Call_UpdatePostsAsync(IEnumerable<Guid> ids)
        {
            var post = _fixture.Build<PostEntity>()
                                .Without(_ => _.Category)
                                .Without(_ => _.Comments)
                                .Without(_ => _.User)
                                .Create();

            _postRepository
                .Setup(_ => _.UpdatePostsAsync(It.IsAny<List<PostEntity>>()))
                .Verifiable();

            _postRepository
                .Setup(_ => _.GetPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(post)
                .Verifiable();

            await _postService.AcceptPostsAsync(ids);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.UpdatePostsAsync(It.IsAny<List<PostEntity>>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task RejectPostsAsync_Should_Call_UpdatePostsAsync(IEnumerable<Guid> ids)
        {
            var post = _fixture.Build<PostEntity>()
                                 .Without(_ => _.Category)
                                 .Without(_ => _.Comments)
                                 .Without(_ => _.User)
                                 .Create();

            _postRepository
                .Setup(_ => _.UpdatePostsAsync(It.IsAny<List<PostEntity>>()))
                .Verifiable();

            _postRepository
                .Setup(_ => _.GetPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(post)
                .Verifiable();

            await _postService.RejectPostsAsync(ids);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.UpdatePostsAsync(It.IsAny<List<PostEntity>>()), Times.Once);
        }


        [Theory]
        [AutoData]
        public async Task DeletePostAsync_Should_Call_DeletePostAsync(Guid id)
        {

            _postRepository
                .Setup(_ => _.DeletePostAsync(It.IsAny<Guid>()))
                .Verifiable();

            await _postService.DeletePostAsync(id);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.DeletePostAsync(It.IsAny<Guid>()), Times.Once);
        }


        [Theory]
        [AutoData]
        public async Task DeletePostsAsync_Should_Call_DeletePostsAsync(IEnumerable<Guid> ids)
        {

            _postRepository
                .Setup(_ => _.DeletePostsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Verifiable();

            await _postService.DeletePostsAsync(ids);

            _postRepository.Verify();
            _postRepository.Verify(_ => _.DeletePostsAsync(It.IsAny<IEnumerable<Guid>>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task GetAcceptedPostsBySearchAsync_Should_Return_AcceptedPosts(string searchString)
        {
            //Arrange
            List<PostEntity> posts = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .With(x => x.PostStatusId, PostStatus.Accepted)
                               .CreateMany()
                               .ToList();

            _postRepository
                .Setup(_ => _.GetAcceptedPostsBySearchAsync(It.IsAny<string>()))
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            var result = await _postService.GetAcceptedPostsBySearchAsync(searchString);

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetAcceptedPostsBySearchAsync(It.IsAny<string>()), Times.Once);

            Assert.All(posts, x => Assert.Equal(PostStatus.Accepted, x.PostStatusId));
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [AutoData]
        public async Task GetAcceptedPostsByCategoryAsync_Should_Return_AcceptedPosts(Guid categoryId)
        {
            //Arrange
            List<PostEntity> posts = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .With(x => x.PostStatusId, PostStatus.Accepted)
                               .CreateMany()
                               .ToList();

            _postRepository
                .Setup(_ => _.GetAcceptedPostsByCategoryAsync(It.IsAny<Guid>()))
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            var result = await _postService.GetAcceptedPostsByCategoryAsync(categoryId);

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetAcceptedPostsByCategoryAsync(It.IsAny<Guid>()), Times.Once);

            Assert.All(posts, x => Assert.Equal(PostStatus.Accepted, x.PostStatusId));
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [AutoData]
        public async Task GetAcceptedPostsByUserAsync_Should_Return_AcceptedPosts(Guid categoryId)
        {
            //Arrange
            List<PostEntity> posts = _fixture
                               .Build<PostEntity>()
                               .Without(_ => _.Comments)
                               .Without(_ => _.User)
                               .Without(_ => _.Category)
                               .With(x => x.PostStatusId, PostStatus.Accepted)
                               .CreateMany()
                               .ToList();

            _postRepository
                .Setup(_ => _.GetAcceptedPostsByUserAsync(It.IsAny<Guid>()))
                .ReturnsAsync(posts)
                .Verifiable();

            //Act
            var result = await _postService.GetAcceptedPostsByUserAsync(categoryId);

            //Assert
            _postRepository.Verify();
            _postRepository.Verify(_ => _.GetAcceptedPostsByUserAsync(It.IsAny<Guid>()), Times.Once);

            Assert.All(posts, x => Assert.Equal(PostStatus.Accepted, x.PostStatusId));
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

    }
}

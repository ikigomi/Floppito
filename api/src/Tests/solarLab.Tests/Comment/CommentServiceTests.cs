using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Moq;
using solarLab.AppServices.Repositories.Comment;
using solarLab.AppServices.Services.Comment;
using solarLab.Contracts.Comment;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using solarLab.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace solarLab.Tests.Comment
{
    public class CommentServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICommentRepository> _commentRepository;
        private readonly Fixture _fixture;

        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            var config = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            _mapper = config.CreateMapper();
            _commentRepository = new Mock<ICommentRepository>();
            _commentService = new CommentService(_mapper, _commentRepository.Object);
            _fixture = new Fixture();

        }



        [Fact]
        public async Task GetCommentsAsync_Should_Return_Comments()
        {
            //Arrange
            var comments = _fixture
                               .Build<CommentEntity>()
                               .Without(_ => _.Post)
                               .Without(_ => _.User)
                               .CreateMany()
                               .ToList();

            _commentRepository
                .Setup(_ => _.GetCommentsAsync())
                .ReturnsAsync(comments)
                .Verifiable();

            //Act
            var result = await _commentService.GetCommentsAsync();

            //Assert
            _commentRepository.Verify();
            _commentRepository.Verify(_ => _.GetCommentsAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [AutoData]
        public async Task GetCommentsByPostAsync_Should_Return_Comments(Guid postId)
        {
            var comments = _fixture
                                .Build<CommentEntity>()
                                .Without(_ => _.Post)
                                .Without(_ => _.User)
                                .CreateMany()
                                .ToList();

            _commentRepository
                .Setup(_ => _.GetCommentsByPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(comments)
                .Verifiable();

            var result = await _commentService.GetCommentsByPostAsync(postId);

            _commentRepository.Verify();
            _commentRepository.Verify(_ => _.GetCommentsByPostAsync(It.IsAny<Guid>()), Times.Once);
            Assert.NotNull(result);
        }

        [Theory]
        [AutoData]
        public async Task AddCommentAsyncc_Should_Return_Comment(CommentDto model)
        {
            var comment = _fixture
                            .Build<CommentEntity>()
                            .Without(_ => _.Post)
                            .Without(_ => _.User)
                            .Create();

            _commentRepository
                .Setup(_ => _.AddCommentAsync(It.IsAny<CommentEntity>()))
                .ReturnsAsync(comment)
                .Verifiable();

            _commentRepository
                .Setup(_ => _.GetCommentByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(comment)
                .Verifiable();

            var result = await _commentService.AddCommentAsync(model);

            _commentRepository.Verify();
            _commentRepository.Verify(_ => _.AddCommentAsync(It.IsAny<CommentEntity>()), Times.Once);
            _commentRepository.Verify(_ => _.GetCommentByIdAsync(It.IsAny<Guid>()), Times.Once);
            Assert.NotNull(result);
        }

        [Theory]
        [AutoData]
        public async Task UpdateCommentAsync_Should_Call_UpdateCommentAsync(CommentDto model)
        {
            _commentRepository
                .Setup(_ => _.UpdateCommentAsync(It.IsAny<CommentEntity>()))
                .Verifiable();

            await _commentService.UpdateCommentAsync(model);

            _commentRepository.Verify();
            _commentRepository.Verify(_ => _.UpdateCommentAsync(It.IsAny<CommentEntity>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task DeleteCommentAsync_Should_Call_DeleteCommentAsync(Guid id)
        {
            _commentRepository
                .Setup(_ => _.DeleteCommentAsync(It.IsAny<Guid>()))
                .Verifiable();

            await _commentService.DeleteCommentAsync(id);

            _commentRepository.Verify();
            _commentRepository.Verify(_ => _.DeleteCommentAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task DeleteCommentsAsync_Should_Call_DeleteCommentsAsync(IEnumerable<Guid> ids)
        {
            _commentRepository
                .Setup(_ => _.DeleteCommentsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Verifiable();

            await _commentService.DeleteCommentsAsync(ids);

            _commentRepository.Verify();
            _commentRepository.Verify(_ => _.DeleteCommentsAsync(It.IsAny<IEnumerable<Guid>>()), Times.Once);
        }

    }
}

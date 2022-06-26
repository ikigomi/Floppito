using AutoMapper;
using Microsoft.EntityFrameworkCore;
using solarLab.AppServices.Repositories.Comment;
using solarLab.Contracts.Comment;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Comment
{
    /// <inheritdoc/>
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        /// <inheritdoc/>
        public async Task<CommentDto> AddCommentAsync(CommentDto model)
        {
            var comment = _mapper.Map<CommentEntity>(model);

            var result = await _commentRepository.AddCommentAsync(comment);
            var commentWithUser = await _commentRepository.GetCommentByIdAsync(result.Id);
            return _mapper.Map<CommentDto>(commentWithUser); 
        }

        /// <inheritdoc/>
        public async Task DeleteCommentAsync(Guid id)
        {
            await _commentRepository.DeleteCommentAsync(id);
        }

        /// <inheritdoc/>
        public async Task DeleteCommentsAsync(IEnumerable<Guid> ids)
        {
            await _commentRepository.DeleteCommentsAsync(ids);
        }

        /// <inheritdoc/>
        public async Task<List<CommentDto>> GetCommentsAsync()
        {
            var comments = await _commentRepository.GetCommentsAsync();
            return comments.Count > 0 ? _mapper.Map<List<CommentDto>>(comments) : new List<CommentDto>();
        }

        /// <inheritdoc/>
        public async Task<List<CommentDto>> GetCommentsByPostAsync(Guid postId)
        {
            var comments = await _commentRepository.GetCommentsByPostAsync(postId);
            return comments.Count > 0 ? _mapper.Map<List<CommentDto>>(comments) : new List<CommentDto>();
        }

        /// <inheritdoc/>
        public async Task UpdateCommentAsync(CommentDto model)
        {
            var comment = _mapper.Map<CommentEntity>(model);

            await _commentRepository.UpdateCommentAsync(comment);
        }
    }
}

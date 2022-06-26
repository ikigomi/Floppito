using AutoMapper;
using Microsoft.EntityFrameworkCore;
using solarLab.AppServices.Repositories.Post;
using solarLab.Contracts.Enums;
using solarLab.Contracts.Post;
using solarLab.Contracts.Search;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Post
{
    /// <inheritdoc />
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }


        /// <inheritdoc />
        public async Task<List<PostDto>> GetAcceptedPostsAsync()
        {
            var result = await _postRepository.GetAcceptedPostsAsync();

            return result.Count > 0 ? _mapper.Map<List<PostDto>>(result) : new List<PostDto>();
        }

        /// <inheritdoc />
        public async Task<List<PostDto>> GetNewPostsAsync()
        {
            var result = await _postRepository.GetNewPostsAsync();

            return result.Count > 0 ? _mapper.Map<List<PostDto>>(result) : new List<PostDto>();
        }

        /// <inheritdoc />
        public async Task<PostDto> GetPostAsync(Guid id)
        {
            var result = await _postRepository.GetPostAsync(id);

            return _mapper.Map<PostDto>(result);
        }

        /// <inheritdoc />
        public async Task<PostDto> AddPostAsync(PostDto model)
        {
            var post = _mapper.Map<PostEntity>(model);
            post.PostStatusId = PostStatus.New;
            var result = await _postRepository.AddPostAsync(post);
            return _mapper.Map<PostDto>(result);

        }


        /// <inheritdoc />
        public async Task UpdatePostAsync(PostDto model)
        {
            var post = _mapper.Map<PostEntity>(model);
            post.PostStatusId = PostStatus.New;
            await _postRepository.UpdatePostAsync(post);
        }

        /// <inheritdoc />
        public async Task DeletePostAsync(Guid id)
        {
            await _postRepository.DeletePostAsync(id);
        }

        /// <inheritdoc />
        public async Task<List<PostDto>> GetAcceptedPostsBySearchAsync(string searchString)
        {
            var result = await _postRepository.GetAcceptedPostsBySearchAsync(searchString);
            return _mapper.Map<List<PostDto>>(result);
        }

        /// <inheritdoc />
        public async Task<List<PostDto>> GetRejectedPostsAsync()
        {
            var result = await _postRepository.GetRejectedPostsAsync();
            return result.Count > 0 ? _mapper.Map<List<PostDto>>(result) : new List<PostDto>();
        }

        /// <inheritdoc />
        public async Task<PostDto> GetPostWithCommentsAsync(Guid id)
        {
            var result = await _postRepository.GetPostWithCommentsAsync(id);
            return _mapper.Map<PostDto>(result);
        }

        /// <inheritdoc />
        public async Task<List<PostDto>> GetAcceptedPostsByCategoryAsync(Guid categoryId)
        {
            var result = await _postRepository.GetAcceptedPostsByCategoryAsync(categoryId);
            return result.Count > 0 ? _mapper.Map<List<PostDto>>(result) : new List<PostDto>();
        }

        /// <inheritdoc />
        public async Task DeletePostsAsync(IEnumerable<Guid> ids)
        {
            await _postRepository.DeletePostsAsync(ids);
        }

        /// <inheritdoc />
        public async Task UpdatePostsAsync(IEnumerable<PostDto> models)
        {
            var posts = _mapper.Map<List<PostEntity>>(models);
            posts.ForEach(p => p.PostStatusId = PostStatus.New);
            await _postRepository.UpdatePostsAsync(posts);
        }

        /// <inheritdoc />
        public async Task AcceptPostsAsync(IEnumerable<Guid> ids)
        {
            var posts = new List<PostEntity>();
            foreach (var id in ids)
            {
                var post = await _postRepository.GetPostAsync(id);
                posts.Add(post);
            }
            posts.ForEach(p => p.PostStatusId = PostStatus.Accepted);
            await _postRepository.UpdatePostsAsync(posts);
        }

        /// <inheritdoc />
        public async Task RejectPostsAsync(IEnumerable<Guid> ids)
        {
            var posts = new List<PostEntity>();
            foreach (var id in ids)
            {
                var post = await _postRepository.GetPostAsync(id);
                posts.Add(post);
            }
            posts.ForEach(p => p.PostStatusId = PostStatus.Rejected);
            await _postRepository.UpdatePostsAsync(posts);
        }

        /// <inheritdoc />
        public async Task<List<PostDto>> GetAcceptedPostsByUserAsync(Guid userId)
        {
            var result = await _postRepository.GetAcceptedPostsByUserAsync(userId);

            return result.Count > 0 ? _mapper.Map<List<PostDto>>(result) : new List<PostDto>();
        }

        public async Task<List<PostDto>> GetAcceptedPostsBySearchAsync(FilterDto filter)
        {
            var result = await _postRepository.GetAcceptedPostsBySearchAsync(filter);

            return _mapper.Map<List<PostDto>>(result);
        }
    }
}

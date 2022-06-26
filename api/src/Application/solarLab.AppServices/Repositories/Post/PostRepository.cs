using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

namespace solarLab.AppServices.Repositories.Post
{
    public class PostRepository : Repository<PostEntity>, IPostRepository
    {

        public PostRepository(DbContext context) : base(context)
        {
        }

        /// <inheritdoc />
        public async Task<List<PostEntity>> GetAcceptedPostsAsync()
        {
            return await GetAllFiltered(x => x.PostStatusId == PostStatus.Accepted)
                .Include(c => c.User)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<List<PostEntity>> GetNewPostsAsync()
        {
            return await GetAllFiltered(x => x.PostStatusId == PostStatus.New)
                .Include(c => c.User)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<PostEntity> GetPostAsync(Guid id)
        {
            return await GetAllFiltered(x => x.Id == id)
                .Include(c => c.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync();
        }

        public async Task<PostEntity> AddPostAsync(PostEntity post)
        {
            return await AddAsync(post);

        }

        public async Task UpdatePostAsync(PostEntity post)
        {
            await UpdateAsync(post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            var post = await GetByIdAsync(id);
            await DeleteAsync(post);
        }

        public async Task<List<PostEntity>> GetAcceptedPostsBySearchAsync(string searchString)
        {
            return await GetAllFiltered(x =>
                x.PostStatusId == PostStatus.Accepted &&
                x.Title.Contains(searchString.Trim()))
                .ToListAsync();
        }

        public async Task<List<PostEntity>> GetRejectedPostsAsync()
        {
            return await GetAllFiltered(x => x.PostStatusId == PostStatus.Rejected)
                .Include(c => c.User)
                .Include(p => p.Category)
                .ToListAsync();

        }

        public async Task<PostEntity> GetPostWithCommentsAsync(Guid id)
        {
            return await GetAllFiltered(x => x.Id == id)
                .Include(c => c.User)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                    .ThenInclude(_ => _.User)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PostEntity>> GetAcceptedPostsByCategoryAsync(Guid categoryId)
        {
            return await GetAllFiltered(x =>
                x.PostStatusId == PostStatus.Accepted &&
                x.CategoryId == categoryId)
                .Include(c => c.User)
                .Include(p => p.Category)
                .ToListAsync();

        }

        public async Task DeletePostsAsync(IEnumerable<Guid> ids)
        {
            var posts = await GetAllFiltered(p => ids.Contains(p.Id))
                .ToListAsync();
            await DeleteAsync(posts);
        }

        public async Task UpdatePostsAsync(IEnumerable<PostEntity> posts)
        {
            await UpdateAsync(posts);
        }

        public async Task<List<PostEntity>> GetAcceptedPostsByUserAsync(Guid userId)
        {
            return await GetAllFiltered(x =>
                x.PostStatusId == PostStatus.Accepted &&
                x.CreatorId == userId)
                .Include(c => c.User)
                .Include(p => p.Category)
                .ToListAsync();

        }

        public async Task<List<PostEntity>> GetAcceptedPostsBySearchAsync(FilterDto filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(FilterDto), "Фильтр поиска был пустым");
            }

            var posts = GetAllFiltered(_ => _.PostStatusId == PostStatus.Accepted);

            if (!string.IsNullOrWhiteSpace(filter.SearchString))
            {
                var searchString = filter.SearchString.Trim();
                switch (filter.SearchIn)
                {
                    case SearchIn.Title:
                        posts = posts.Where(_ => _.Title.Contains(searchString));
                        break;
                    case SearchIn.Description:
                        posts = posts.Where(_ => _.Description.Contains(searchString));
                        break;
                    case SearchIn.Everywrehe:
                        posts = posts.Where(_ => _.Title.Contains(searchString) || _.Description.Contains(searchString));
                        break;

                    default:
                        break;
                }
            }

            if (filter.WorkExperience != null)
            {
                foreach (var item in filter.WorkExperience)
                {
                    posts = posts.ToList().Where(_ => _.WorkExperience.Contains(item)).AsQueryable();
                }
            }


            if (filter.WorkSchedule != null)
            {
                foreach (var item in filter.WorkSchedule)
                {
                    posts = posts.ToList().Where(_ => _.WorkSchedule.Contains(item)).AsQueryable();
                }
            }



            if (filter.SalaryFrom != null)
                posts = posts.Where(_ => _.SalaryFrom >= filter.SalaryFrom);

            if (filter.SalaryTo != null)
                posts = posts.Where(_ => _.SalaryTo <= filter.SalaryTo);

            switch (filter.SortBy)
            {
                case Sorting.ByTitle:
                    posts = posts.OrderBy(_ => _.Title);
                    break;
                case Sorting.BySalary:
                    posts = posts.OrderBy(_ => _.SalaryFrom);
                    break;
                case Sorting.ByCreationDate:
                    posts = posts.OrderBy(_ => _.CreationDate);
                    break;

                default:
                    break;
            }

            return posts.ToList();

        }

    }
}

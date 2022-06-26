using Microsoft.EntityFrameworkCore;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Comment
{
    public class CommentRepository:Repository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context)
        {
        }

        public async Task<CommentEntity> AddCommentAsync(CommentEntity comment)
        {
            return await AddAsync(comment);
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await GetByIdAsync(id);
            await DeleteAsync(comment);
        }

        public async Task DeleteCommentsAsync(IEnumerable<Guid> ids)
        {
            var comments = await GetAllFiltered(c => ids.Contains(c.Id))
                .ToListAsync();
            await DeleteAsync(comments);
        }

        public async Task<CommentEntity> GetCommentByIdAsync(Guid commentId)
        {
            return await GetAllFiltered(x => x.Id == commentId)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync();
        }

        public async Task<List<CommentEntity>> GetCommentsAsync()
        {
            return await GetAll()
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<List<CommentEntity>> GetCommentsByPostAsync(Guid postId)
        {
            return await GetAllFiltered(c => c.PostId == postId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task UpdateCommentAsync(CommentEntity comment)
        {
            await UpdateAsync(comment);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Chat
{
    public class ChatRepository : Repository<ChatEntity>, IChatRepository
    {
        public ChatRepository(DbContext context):base(context)
        {

        }

        public async Task<ChatEntity> AddChatAsync(ChatEntity chat)
        {
            return await AddAsync(chat);
        }

        public async Task<ChatEntity> GetChatByIdAsync(Guid id)
        {
            return await GetAllFiltered(_ => _.Id == id)
                .Include(_ => _.Messages)
                .Include(_ => _.Members)
                .FirstOrDefaultAsync();
        }

        public Guid GetChatIdByUsers(IEnumerable<ApplicationUser> users)
        {
            return GetAllFiltered(c => c.Members.Count == users.Count())
                .Include(_ => _.Members)
                .AsEnumerable()
                .Where(c=> Array.Exists(users.ToArray(), user => c.Members.Contains(user)))
                .Select(_=>_.Id)
                .FirstOrDefault();
        }

        public async Task<List<ChatEntity>> GetChatsByUserAsync(ApplicationUser user)
        {

            return await GetAllFiltered(c => c.Members.Contains(user) && c.Messages.Count>0)
                .Include(_ => _.Messages.OrderByDescending(_=>_.CreationDate).Take(1))
                .Include(_ => _.Members)
                .ToListAsync();
        }

        public async Task UpdateChatAsync(ChatEntity chat)
        {
            await UpdateAsync(chat);
        }
    }
}

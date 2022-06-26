using solarLab.Contracts.Message;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Chat
{
    public interface IChatRepository:IRepository<ChatEntity>
    {
        Task<List<ChatEntity>> GetChatsByUserAsync(ApplicationUser user);
        Guid GetChatIdByUsers(IEnumerable<ApplicationUser> users);
        Task<ChatEntity> GetChatByIdAsync(Guid id);
        Task<ChatEntity> AddChatAsync(ChatEntity chat);
        Task UpdateChatAsync(ChatEntity chat);
    }
}

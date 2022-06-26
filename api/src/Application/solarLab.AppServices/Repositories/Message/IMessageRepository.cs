using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Message
{
    public interface IMessageRepository:IRepository<MessageEntity>
    {
        Task DeleteMessageAsync(Guid id);
        Task UpdateMessageAsync(MessageEntity message);
        Task UpdateMessagesAsync(IEnumerable<MessageEntity> messages);
        Task<MessageEntity> AddMessageAsync(MessageEntity message);
    }
}

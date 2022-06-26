using Microsoft.EntityFrameworkCore;
using solarLab.Domain.Entities;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Repositories.Message
{
    public class MessageRepository : Repository<MessageEntity>, IMessageRepository
    {
        public MessageRepository(DbContext context):base(context)
        {

        }

        public async Task<MessageEntity> AddMessageAsync(MessageEntity message)
        {
            var result = await AddAsync(message);
            return await GetAllFiltered(_ => _.Id == result.Id)
                .Include(_ => _.Sender)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            var message = await GetByIdAsync(id);
            await DeleteAsync(message);
        }

        public async Task UpdateMessageAsync(MessageEntity message)
        {
            await UpdateAsync(message);
        }

        public async Task UpdateMessagesAsync(IEnumerable<MessageEntity> messages)
        {
            await UpdateAsync(messages);
        }
    }
}

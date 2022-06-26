using AutoMapper;
using solarLab.AppServices.Repositories.Message;
using solarLab.Contracts.Message;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Message
{
    /// <inheritdoc/>
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<MessageDto> AddMessageAsync(MessageDto model)
        {
            var message = _mapper.Map<MessageEntity>(model);
            var result = await _messageRepository.AddMessageAsync(message);
            return _mapper.Map<MessageDto>(result);
        }

        /// <inheritdoc/>
        public async Task DeleteMessageAsync(Guid id)
        {
            await _messageRepository.DeleteMessageAsync(id);
        }

        /// <inheritdoc/>
        public async Task UpdateMessageAsync(MessageDto model)
        {
            var message = _mapper.Map<MessageEntity>(model);
            await _messageRepository.UpdateMessageAsync(message);
        }
    }
}

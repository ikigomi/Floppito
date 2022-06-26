using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using solarLab.AppServices.Repositories.Chat;
using solarLab.Contracts.Chat;
using solarLab.Contracts.Message;
using solarLab.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.Chat
{
    /// <inheritdoc />
    public class ChatService:IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;



        public ChatService(IChatRepository chatRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<ChatDto> AddChatAsync(ChatDto model)
        {
            var chat = _mapper.Map<ChatEntity>(model);
            chat.Members = chat.Members.Select(u => _userManager.FindByIdAsync(u.Id.ToString()).Result).ToList();
            var result = await _chatRepository.AddChatAsync(chat);
            return _mapper.Map<ChatDto>(result);
        }

        /// <inheritdoc/>
        public async Task AddUserToChatAsync(Guid chatId, Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<ChatDto> GetChatByIdAsync(Guid id)
        {
            var result = await _chatRepository.GetChatByIdAsync(id);
            return _mapper.Map<ChatDto>(result);
        }

        /// <inheritdoc/>
        public Guid GetChatIdByUsers(IEnumerable<Guid> userIds)
        {
            var users = new List<ApplicationUser>();
            foreach (var userId in userIds)
            {
                var user = _userManager.FindByIdAsync(userId.ToString()).Result;
                users.Add(user);
            }
            return _chatRepository.GetChatIdByUsers(users.ToArray());
        }

        /// <inheritdoc/>
        public async Task<List<ChatDto>> GetChatsByUserAsync()
        {
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result =  await _chatRepository.GetChatsByUserAsync(user);
            return result.Count > 0 ? _mapper.Map<List<ChatDto>>(result) : new List<ChatDto>();
        }
    }
}

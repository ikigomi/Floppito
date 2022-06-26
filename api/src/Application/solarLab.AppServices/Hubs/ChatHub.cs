using Microsoft.AspNetCore.SignalR;
using solarLab.AppServices.Services.Message;
using solarLab.Contracts.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task NewMessage(MessageDto model)
        {
            var message = await _messageService.AddMessageAsync(model);
            await Clients.All.SendAsync("received", message);
        }
    }
}

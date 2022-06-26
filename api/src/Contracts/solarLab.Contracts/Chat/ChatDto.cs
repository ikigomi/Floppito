using solarLab.Contracts.Base;
using solarLab.Contracts.Message;
using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Chat
{
    /// <summary>
    /// Модель чата
    /// </summary>
    public class ChatDto : BaseDto
    {
        /// <summary>
        /// Коллекция сообщений
        /// </summary>
        public List<MessageDto> Messages { get; set; }
        /// <summary>
        /// Коллекция участников
        /// </summary>
        public List<AuthorDto> Members { get; set; }
    }
}

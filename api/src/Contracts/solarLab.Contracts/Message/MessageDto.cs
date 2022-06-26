using solarLab.Contracts.Base;
using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Message
{
    /// <summary>
    /// Модель сообщения
    /// </summary>
    public class MessageDto : BaseDto
    {
        /// <summary>
        /// Идентификатор чата
        /// </summary>
        public Guid ChatId { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        public AuthorDto Author { get; set; }
    }
}

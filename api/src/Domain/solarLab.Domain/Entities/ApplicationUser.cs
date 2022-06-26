using Microsoft.AspNetCore.Identity;
using solarLab.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Domain.Entities
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Пол.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Аватарка пользователя.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public ICollection<string> Roles { get; set; }



        public ICollection<ChatEntity> Chats { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
        public ICollection<MessageEntity> Messages { get; set; }
        public ICollection<PostEntity> Posts { get; set; }

    }
}

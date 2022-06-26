using solarLab.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.User
{
    /// <summary>
    /// Модель автора сущности
    /// </summary>
    public class AuthorDto:BaseDto
    {
        /// <summary>
        /// Имя автора объявления.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Аватарка автора объявления.
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}

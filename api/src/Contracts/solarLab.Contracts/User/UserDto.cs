using solarLab.Contracts.Base;
using solarLab.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.User
{
    /// <summary>
    /// Модель представления пользователя
    /// </summary>
    public class UserDto : BaseDto
    {
        /// <summary>
        /// Логин
        /// </summary>  
        public string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>  
        public string Name { get; set; }

        /// <summary>
        /// Адресс почты
        /// </summary>  
        public string Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>  
        public string PhoneNumber { get; set; }

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
        public Roles[] Roles { get; set; }
    }
}

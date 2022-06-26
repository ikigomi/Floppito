using solarLab.Contracts.Base;
using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Comment
{
    /// <summary>
    /// Модель представления комментариев к объявлению.
    /// </summary>
    public class CommentDto : BaseDto
    {
        /// <summary>
        /// Идентификатор объявления.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Тело комментария.
        /// </summary>
        public string CommentBody { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        public AuthorDto Author { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}

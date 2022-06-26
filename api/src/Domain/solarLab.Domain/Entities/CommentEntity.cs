using solarLab.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Domain.Entities
{
    /// <summary>
    /// Комментарии к объявлению.
    /// </summary>
    public class CommentEntity : BaseEntity
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
        /// Пользователь, создавший комментарий
        /// </summary>
        public Guid CreatorId { get; set; }


        /// <summary>
        /// Объявление.
        /// </summary>
        public PostEntity Post { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public ApplicationUser User { get; set; }
    }
}

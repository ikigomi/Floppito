using solarLab.Contracts.Enums;
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
    /// Модель объявления.
    /// </summary>
    public class PostEntity : BaseEntity
    {
        /// <summary>
        /// Заголовок объявления.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание объявления.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public PostStatus PostStatusId { get; set; }

        /// <summary>
        /// Пользователь, создавший объявление
        /// </summary>
        public Guid CreatorId { get; set; }

        /// <summary>
        /// Идентификатор объявления
        /// </summary>

        public Guid CategoryId { get; set; }


        /// <summary>
        /// Коллекция комментариев.
        /// </summary>
        public ICollection<CommentEntity> Comments { get; set; }

        /// <summary>
        /// Картинки объявления.
        /// </summary>
        public IEnumerable<string> Images { get; set; }

        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }

        public IEnumerable<Experience> WorkExperience { get; set; }

        public IEnumerable<Schedule> WorkSchedule { get; set; }
        public MoneyCode MoneyCode { get; set; }


        /// <summary>
        /// Пользователь.
        /// </summary>
        public ApplicationUser User { get; set; }


        public CategoryEntity Category { get; set; }

    }
}

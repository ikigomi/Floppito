using solarLab.Contracts.Base;
using solarLab.Contracts.Category;
using solarLab.Contracts.Comment;
using solarLab.Contracts.Enums;
using solarLab.Contracts.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Post
{
    /// <summary>
    /// Модель объявления
    /// </summary>
    public class PostDto : BaseDto
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
        /// Автор
        /// </summary>
        public AuthorDto Author { get; set; }

        /// <summary>
        /// Идентификатор категории.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string CategoryTitle { get; set; }

        /// <summary>
        /// Картинки объявления.
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Коллекция комментариев.
        /// </summary>
        public List<CommentDto> Comments { get; set; }

        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }

        public List<Experience> WorkExperience { get; set; }

        public List<Schedule> WorkSchedule { get; set; }

        public MoneyCode? MoneyCode { get; set; }
    }
}

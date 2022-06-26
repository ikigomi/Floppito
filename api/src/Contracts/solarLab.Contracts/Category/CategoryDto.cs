using solarLab.Contracts.Base;
using solarLab.Contracts.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Category
{
    /// <summary>
    /// Модель категории
    /// </summary>
    public class CategoryDto:BaseDto
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Title { get; set; }
    }
}

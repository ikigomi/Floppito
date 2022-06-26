using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Domain.Base
{
    /// <summary>
    /// Базовый класс сущностей.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}

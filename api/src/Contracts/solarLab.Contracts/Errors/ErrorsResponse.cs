using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Errors
{
    /// <summary>
    /// Модель ответа при возникновении исключений
    /// </summary>
    public class ErrorsResponse
    {
        /// <summary>
        /// Ошибки
        /// </summary>
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}

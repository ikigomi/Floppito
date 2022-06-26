using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.Contracts.Image
{
    /// <summary>
    /// Модель с коллекцией путей к картинкам для постов
    /// </summary>
    public class ImagePathsResponse
    {
        /// <summary>
        /// Пути к картинкам
        /// </summary>
        public List<string> Paths { get; set; }
    }
}

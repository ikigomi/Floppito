using Microsoft.AspNetCore.Http;
using solarLab.Contracts.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.File
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<ImagePathsResponse> UploadAsync(IEnumerable<IFormFile> data);
    }
}

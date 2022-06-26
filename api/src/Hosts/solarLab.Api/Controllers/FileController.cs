using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using solarLab.AppServices.Services.File;
using solarLab.Infrastructure.Exceptions;
using solarLab.Infrastructure.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarLab.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с файлами
    /// </summary>
    public class FileController : ApiController
    {
        private readonly IFileService _fileService;

        public FileController(IFileService imageService)
        {
            _fileService = imageService;
        }

        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile[] data)
        {
            if (Array.Exists(data, _ => !_.ContentType.Contains("image")))
                throw new InvalidFileFormatException("Неверный формат файла");
            var paths = await _fileService.UploadAsync(data);
            return Ok(paths);
        }
    }
}

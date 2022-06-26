using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using solarLab.AppServices.Options;
using solarLab.Contracts.Image;
using solarLab.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace solarLab.AppServices.Services.File
{
    /// <inheritdoc/>
    public class FileService : IFileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        /// <inheritdoc/>
        public async Task<ImagePathsResponse> UploadAsync(IEnumerable<IFormFile> data)
        {
            var hostUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var paths = new ImagePathsResponse { Paths = new List<string>() };

            foreach (var file in data)
            {
                var fileName = Guid.NewGuid().ToString() + "__" + DateTime.Today.ToShortDateString() + "__" + file.FileName;

                var folderName = Path.Combine("images", "posts");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
                var fullPath = Path.Combine(pathToSave, fileName);

                await using var fileStream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                var pathToImage = Path.Combine("https://", hostUrl, folderName, fileName);
                paths.Paths.Add(pathToImage);
            }

            return paths;
        }
    }
}

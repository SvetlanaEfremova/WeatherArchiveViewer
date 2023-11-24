using BusinessLogic.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<List<string>> UploadFiles(List<FileUploadDto> files)
        {
            var uploadedFileNames = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            CreateNewDirectory(uploadPath);
            foreach (var file in files)
            {
                try
                {
                    var upladedFileName = await UploadFile(file, uploadPath);
                    if (!string.IsNullOrEmpty(upladedFileName))
                        uploadedFileNames.Add(upladedFileName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while uploading file: {FileName}", file.FileName);
                }
            }
            return uploadedFileNames;
        }

        public void CreateNewDirectory(string uploadPath)
        {
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
        }

        public async Task<string> UploadFile(FileUploadDto file, string uploadPath)
        {
            if (file.FileStream.Length > 0)
            {
                var filePath = Path.Combine(uploadPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(stream);
                }
                return filePath;
            }
            return null;
        }
    }
}

using BusinessLogic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class FileService
    {
        public async Task<List<string>> UploadFiles(List<FileUploadDto> files)
        {
            var uploadedFileNames = new List<string>();
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.FileStream.Length > 0)
                {
                    var filePath = Path.Combine(uploadPath, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.FileStream.CopyToAsync(stream);
                    }
                    uploadedFileNames.Add(filePath);
                }
            }
            return uploadedFileNames;
        }
    }
}

using BusinessLogic.DTO;

namespace Web.Converters
{
    public class FileUploadDtoConverter
    {
        public static List<FileUploadDto> ConvertFormFileListToDto(List<IFormFile> files)
        {
            var fileDtos = new List<FileUploadDto>();
            foreach (var file in files)
            {
                var fileDto = new FileUploadDto
                {
                    FileStream = file.OpenReadStream(),
                    FileName = file.FileName
                };
                fileDtos.Add(fileDto);
            }
            return fileDtos;
        }
    }
}

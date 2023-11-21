using BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTO;
using Web.Converters;
using BusinessLogic.Parsers;

namespace Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly FileService fileService;
        private readonly IWebHostEnvironment _environment;
        public WeatherController(FileService fileService, IWebHostEnvironment environment)
        {
            this.fileService = fileService;
            _environment = environment;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var fileDtos = FileUploadDtoConverter.ConvertFormFileListToDto(files);
            await fileService.UploadFiles(fileDtos);
            //обновление базы данных
            string uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads");
            string filePath = Path.Combine(uploadsFolder, "moskva_2010.xlsx");
            ExcelParser.ParseWeatherData(filePath);
            return Json(new { message = "The files were uploaded" });
        }
    }
}

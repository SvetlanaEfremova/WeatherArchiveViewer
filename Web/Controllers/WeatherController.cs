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
        private readonly WeatherService _weatherService;
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> ViewArchives()
        {
            var weatherData = await _weatherService.GetWeatherData();
            return View(weatherData);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            var fileDtos = FileUploadDtoConverter.ConvertFormFileListToDto(files);
            await _weatherService.AddNewData(fileDtos);
            return Json(new { message = "The files were uploaded" });
        }
    }
}

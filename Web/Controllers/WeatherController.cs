using BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTO;
using Web.Converters;
using BusinessLogic.Parsers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;

namespace Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> ViewArchives(int? month, int? year, int page = 1, int pageSize = 10)
        {
            var weatherData = await _weatherService.GetWeatherData(month, year);

            var count = weatherData.Count();
            var items = weatherData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            WeatherViewModel viewModel = new WeatherViewModel
            {
                WeatherData = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
            };

            return View(viewModel);
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

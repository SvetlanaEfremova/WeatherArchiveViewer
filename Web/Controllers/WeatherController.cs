using BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.DTO;
using Web.Converters;
using BusinessLogic.Parsers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;

        private readonly WeatherService _weatherService;

        public WeatherController(ILogger<WeatherController> logger, WeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> ViewArchives(int? month, int? year, int page = 1, int pageSize = 10)
        {
            try
            {
                var weatherData = await _weatherService.GetWeatherData(month, year);
                var availableYears = await _weatherService.GetAvailableYears();
                var count = weatherData.Count();
                var items = weatherData.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var viewModel = CreateWeatherViewModel(month, year, page, pageSize, count, availableYears, items);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving archive data");
                return View("Error", new ErrorViewModel { ErrorMessage = "Error occurred while retrieving archive weather data" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var fileDtos = FileUploadDtoConverter.ConvertFormFileListToDto(files);
                await _weatherService.AddNewData(fileDtos);
                return Json(new { message = "The files were uploaded" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
                return Json(new { message = "Error occurred while adding data" });
            }
        }

        private WeatherViewModel CreateWeatherViewModel(int? month, int? year, int page, int pageSize, int count, List<int> availableYears, IEnumerable<Infrastructure.Models.Weather> items)
        {
            return new WeatherViewModel
            {
                WeatherData = items,
                SelectedMonth = month,
                SelectedYear = year,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
                AvailableYears = availableYears,
            };
        }

    }
}

using Infrastructure.Models;

namespace Web.ViewModels
{
    public class WeatherViewModel
    {
        public IEnumerable<Weather> WeatherData { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int? SelectedMonth { get; set; }

        public int? SelectedYear { get; set; }

        public List<int>? AvailableYears { get; set; }
    }
}

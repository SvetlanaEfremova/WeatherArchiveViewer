using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Weather
    {
        public int Id { get; set; }

        public DateTime DateAndTime { get; set; }

        [Precision(3, 1)]
        public decimal Temperature { get; set; }

        [Precision(4, 2)]
        [Range(0.0, 100.0)]
        public decimal Humidity { get; set; }

        [Precision(3, 1)]
        public decimal DewPoint { get; set; }

        public int AtmosphericPressure { get; set; }

        public string? WindDirection { get; set; }

        public int? WindVelocity { get; set; }

        [Range(0, 100)]
        public int? Cloudiness { get; set; }

        public int LowerCloudLimit { get; set; }

        public int? HorizontalVisibility { get; set; }

        public string? WeatherEvents { get; set; }
    }
}
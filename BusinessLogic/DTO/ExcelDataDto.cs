using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class ExcelDataDto
    {
        public DateTime DateAndTime { get; set; }

        public decimal? Temperature { get; set; }

        public int? Humidity { get; set; }

        public decimal? DewPoint { get; set; }

        public int? AtmosphericPressure { get; set; }

        public string? WindDirection { get; set; }

        public int? WindVelocity { get; set; }

        public int? Cloudiness { get; set; }

        public int? LowerCloudLimit { get; set; }

        public int? HorizontalVisibility { get; set; }

        public string? WeatherEvents { get; set; }
    }
}

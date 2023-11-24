using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WeatherRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<WeatherRepository> _logger;

        public WeatherRepository(ApplicationDbContext dbContext, ILogger<WeatherRepository> logger) 
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddDataToDb (List<Weather> weatherData)
        {
            try
            {
                await _dbContext.AddRangeAsync(weatherData);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding files to database");
            }
        }

        public async Task<List<Weather>> GetWeatherData(int? month, int? year)
        {
            var query = _dbContext.WeatherData.OrderBy(w => w.DateAndTime).AsQueryable();
            if (month.HasValue)
                query = FilterWeatherDataByMonth(query, month.Value);
            if (year.HasValue)
                query = FilterWeatherDataByYear(query, year.Value);
            var weatherData = await query.ToListAsync();
            return weatherData;
        }

        public async Task<List<int>> GetAvailableYears()
        {
            var years = await _dbContext.WeatherData
                .Select(w => w.DateAndTime.Year)
                .Distinct()
                .OrderBy(year => year)
                .ToListAsync();
            return years;
        }

        private IQueryable<Weather> FilterWeatherDataByMonth(IQueryable<Weather> query, int month)
        {
            return query.Where(w => w.DateAndTime.Month == month);
        }

        private IQueryable<Weather> FilterWeatherDataByYear(IQueryable<Weather> query, int year)
        {
            return query.Where(w => w.DateAndTime.Year == year);
        }
    }
}

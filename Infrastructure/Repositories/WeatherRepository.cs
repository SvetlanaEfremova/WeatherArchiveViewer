using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
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
        public WeatherRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task AddDataToDb (List<Weather> weatherData)
        {
            await _dbContext.AddRangeAsync(weatherData);
            await _dbContext.SaveChangesAsync();
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

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

        public async Task<List<Weather>> GetWeatherData()
        {
            return await _dbContext.WeatherData.ToListAsync();
        }
    }
}

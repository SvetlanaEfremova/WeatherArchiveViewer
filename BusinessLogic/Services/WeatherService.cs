using BusinessLogic.DTO;
using BusinessLogic.Parsers;
using Infrastructure.Models;
using Infrastructure.Repositories;
using NPOI.POIFS.Storage;
using NPOI.SS.Formula.Functions;
using System;

namespace BusinessLogic.Services
{
    public class WeatherService
    {
        private readonly FileService _fileService;

        private readonly ExcelParser _excelParser;

        private readonly WeatherRepository _weatherRepository;

        public WeatherService(FileService fileService, ExcelParser excelParser, WeatherRepository weatherRepository) 
        {
            _fileService = fileService;
            _excelParser = excelParser;
            _weatherRepository = weatherRepository;
        }

        public async Task AddNewData(List<FileUploadDto> files)
        {
            var uploadedFileNames = await _fileService.UploadFiles(files);
            var excelDataFromFiles = _excelParser.ParseWeatherDataFromFiles(uploadedFileNames);
            var weatherData = ConvertExcelDataToWeatherData(excelDataFromFiles);
            await _weatherRepository.AddDataToDb(weatherData);
        }

        public async Task<List<Weather>> GetWeatherData(int? month, int? year)
        {
            return await _weatherRepository.GetWeatherData(month, year);
        }

        public async Task<List<int>> GetAvailableYears()
        {
            return await _weatherRepository.GetAvailableYears();
        }

        private List<Weather> ConvertExcelDataToWeatherData(List<ExcelDataDto> excelData)
        {
            var weatherData = new List<Weather>();
            foreach (var item in excelData)
            {
                if (item == null) continue;
                var weather = ConvertExcelToWeather(item);
                weatherData.Add(weather);
            }
            return weatherData;
        }

        private Weather ConvertExcelToWeather(ExcelDataDto excelDto)
        {
            return new Weather
            {
                DateAndTime = excelDto.DateAndTime,
                Temperature = excelDto.Temperature,
                Humidity = excelDto.Humidity,
                DewPoint = excelDto.DewPoint,
                AtmosphericPressure = excelDto.AtmosphericPressure,
                WindDirection = excelDto.WindDirection,
                WindVelocity = excelDto.WindVelocity,
                Cloudiness = excelDto.Cloudiness,
                LowerCloudLimit = excelDto.LowerCloudLimit,
                HorizontalVisibility = excelDto.HorizontalVisibility,
                WeatherEvents = excelDto.WeatherEvents,
            };
        }
    }
}
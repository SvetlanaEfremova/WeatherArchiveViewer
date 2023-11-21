using BusinessLogic.DTO;
using BusinessLogic.Parsers;
using Infrastructure.Models;
using Infrastructure.Repositories;
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
            var weatherData = _excelParser.ParseWeatherData(uploadedFileNames);
            await _weatherRepository.AddDataToDb(weatherData);
        }

        public async Task<List<Weather>> GetWeatherData()
        {
            return await _weatherRepository.GetWeatherData();
        }
    }
}
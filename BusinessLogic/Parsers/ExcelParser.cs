using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO;
using Infrastructure.Models;
using Microsoft.VisualBasic;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BusinessLogic.Parsers
{
    public class ExcelParser
    {
        public List<ExcelDataDto> ParseWeatherDataFromFiles (List<string> fileNames)
        {
            var excelDataFromFiles = new List<ExcelDataDto>();
            foreach (var fileName in fileNames)
            {
                var excelDataFromFile = ParseWeatherDataFromFile(fileName);
                excelDataFromFiles.AddRange(excelDataFromFile);
            }
            return excelDataFromFiles;
        }

        public List<ExcelDataDto> ParseWeatherDataFromFile(string filePath)
        {
            IWorkbook workbook;
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            return GetDataFromWorkbook(workbook);
        }

        private List<ExcelDataDto> GetDataFromWorkbook(IWorkbook workbook)
        {
            var excelDataFromWorkbook = new List<ExcelDataDto>();
            for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                AddDataFromSheet(sheet, excelDataFromWorkbook);
            }
            return excelDataFromWorkbook;
        }

        private void AddDataFromSheet(ISheet sheet, List<ExcelDataDto> excelDataFromFile)
        {
            for (int rowNum = 4; rowNum <= sheet.LastRowNum; rowNum++)
            {
                IRow currentRow = sheet.GetRow(rowNum);
                if (currentRow != null)
                {
                    var excelDataFromRow = GetDataFromRow(currentRow);
                    if (excelDataFromRow != null)
                        excelDataFromFile.Add(excelDataFromRow);
                }
            }
        }

        private ExcelDataDto GetDataFromRow(IRow row)
        {
            ICell dateCell = row.GetCell(0);
            ICell timeCell = row.GetCell(1);
            if (!TryParseDateTime(dateCell, timeCell, out DateTime dateTime))
                return null;
            return new ExcelDataDto
            {
                DateAndTime = dateTime,
                Temperature = GetDecimalCellValue(row.GetCell(2)),
                Humidity = GetIntCellValue(row.GetCell(3)),
                DewPoint = GetDecimalCellValue(row.GetCell(4)),
                AtmosphericPressure = GetIntCellValue(row.GetCell(5)),
                WindDirection = GetStringCellValue(row.GetCell(6)),
                WindVelocity = GetIntCellValue(row.GetCell(7)),
                Cloudiness = GetIntCellValue(row.GetCell(8)),
                LowerCloudLimit = GetIntCellValue(row.GetCell(9)),
                HorizontalVisibility = GetIntCellValue(row.GetCell(10)),
                WeatherEvents = GetStringCellValue(row.GetCell(11)),
            };
        }

        private bool TryParseDateTime(ICell dateCell, ICell timeCell, out DateTime dateTime)
        {
            if (!TryParseDateCell(dateCell, out DateTime date) || !TryParseTimeCell(timeCell, out TimeSpan time))
            {
                dateTime = default;
                return false;
            }
            dateTime = date.Date.Add(time);
            return true;
        }

        private bool TryParseDateCell(ICell cell, out DateTime date)
        {
            if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
            {
                date = cell.DateCellValue;
                return true;
            }
            else if (cell.CellType == CellType.String)
                return DateTime.TryParseExact(cell.StringCellValue, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            date = default;
            return false;
        }

        private bool TryParseTimeCell(ICell cell, out TimeSpan time)
        {
            if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
            {
                time = cell.DateCellValue.TimeOfDay;
                return true;
            }
            else if (cell.CellType == CellType.String)
                return TimeSpan.TryParseExact(cell.StringCellValue, "HH:mm", CultureInfo.InvariantCulture, out time);
            time = default;
            return false;
        }

        private decimal? GetDecimalCellValue(ICell cell)
        {
            return cell != null && cell.CellType == CellType.Numeric ? (decimal)cell.NumericCellValue : null;
        }

        private int? GetIntCellValue(ICell cell)
        {
            return cell != null && cell.CellType == CellType.Numeric ? (int)cell.NumericCellValue : null;
        }

        private string? GetStringCellValue(ICell cell)
        {
            return cell != null && cell.CellType == CellType.String ? cell.StringCellValue : null;
        }
    }
}

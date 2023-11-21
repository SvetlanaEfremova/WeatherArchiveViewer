using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BusinessLogic.Parsers
{
    public class ExcelParser
    {
        public static List<Weather> ParseWeatherData(string filePath)
        {
            var weathers = new List<Weather>();
            IWorkbook workbook;

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }

            for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                for (int row = 4; row <= sheet.LastRowNum; row++)
                {
                    IRow currentRow = sheet.GetRow(row);

                    if (currentRow != null)
                    {
                        DateTime dateValue = DateTime.MinValue; 
                        DateTime timeValue = DateTime.MinValue;

                        if (currentRow.GetCell(0).CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(currentRow.GetCell(0)))
                        {
                            dateValue = currentRow.GetCell(0).DateCellValue;
                        }
                        else if (currentRow.GetCell(0).CellType == CellType.String)
                        {
                            string dateString = currentRow.GetCell(0).StringCellValue;
                            DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue);
                        }

                        if (currentRow.GetCell(1).CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(currentRow.GetCell(1)))
                        {
                            timeValue = currentRow.GetCell(1).DateCellValue;
                        }
                        else if (currentRow.GetCell(1).CellType == CellType.String)
                        {
                            string timeString = currentRow.GetCell(1).StringCellValue;
                            DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out timeValue);
                        }

                        var weather = new Weather
                        {
                            DateAndTime = new DateTime(dateValue.Year, dateValue.Month, dateValue.Day,
                                             timeValue.Hour, timeValue.Minute, timeValue.Second),

                            Temperature = currentRow.GetCell(2) != null && currentRow.GetCell(2).CellType == CellType.Numeric
                                ? (decimal)currentRow.GetCell(2).NumericCellValue : 0, 

                            Humidity = currentRow.GetCell(3) != null && currentRow.GetCell(3).CellType == CellType.Numeric
                                ? (decimal)currentRow.GetCell(3).NumericCellValue : 0, 

                            DewPoint = currentRow.GetCell(4) != null && currentRow.GetCell(4).CellType == CellType.Numeric
                                ? (decimal)currentRow.GetCell(4).NumericCellValue : 0, 

                            AtmosphericPressure = currentRow.GetCell(5) != null && currentRow.GetCell(5).CellType == CellType.Numeric
                                ? (int)currentRow.GetCell(5).NumericCellValue : 0, 

                            WindDirection = currentRow.GetCell(6) != null && currentRow.GetCell(6).CellType == CellType.String
                                ? currentRow.GetCell(6).StringCellValue : null,

                            WindVelocity = currentRow.GetCell(7) != null && currentRow.GetCell(7).CellType == CellType.Numeric
                                ? (int?)currentRow.GetCell(7).NumericCellValue : null,

                            Cloudiness = currentRow.GetCell(8) != null && currentRow.GetCell(8).CellType == CellType.Numeric
                                ? (int?)currentRow.GetCell(8).NumericCellValue : null,

                            LowerCloudLimit = currentRow.GetCell(9) != null && currentRow.GetCell(9).CellType == CellType.Numeric
                                ? (int)currentRow.GetCell(9).NumericCellValue : 0, 

                            HorizontalVisibility = currentRow.GetCell(10) != null && currentRow.GetCell(10).CellType == CellType.Numeric
                                ? (int?)currentRow.GetCell(10).NumericCellValue : null,

                            WeatherEvents = currentRow.GetCell(11) != null && currentRow.GetCell(11).CellType == CellType.String
                                ? currentRow.GetCell(11).StringCellValue : null
                        };
                        weathers.Add(weather);
                    }
                }
            }
            return weathers;
        }
    }
}

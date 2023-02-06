using LogService2023.App.Dtos;
using LogService2023.App.Models;
using LogService2023.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Net.Mime;

namespace LogService2023.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("GetLogs")]
        public async Task<IEnumerable<Log>> GetLogs([FromQuery]LogFilter logFilter)
        {
            return await _logService.List(logFilter);
        }

        [HttpPost("Create")]
        public async Task<Log> Create(CreateLogDto createLogDto)
        {
            return await _logService.Create(createLogDto);
        }

        [HttpDelete("Delete")]
        public async Task Delete(DateTimeOffset dateTime)
        {
            await _logService.Delete(dateTime);
        }

        [HttpGet("Export")]
        public async Task<FileContentResult> Export([FromQuery] LogFilter logFilter)
        {
            FileContentResult? result = null;
            var logList = await _logService.List(logFilter);
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var package = new ExcelPackage(ms))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LogExport");

                        int row = 1;
                        void setCell(int col, object value) => worksheet.Cells[row, col].Value = value;

                        setCell(1, "Id");
                        setCell(2, "LogType");
                        setCell(3, "TimeStamp");
                        setCell(4, "Description");

                        row = 2;
                        foreach (var item in logList)
                        {
                            setCell(1, item.Id);
                            setCell(2, item.LogType);
                            setCell(3, item.TimeStamp);
                            setCell(4, item.Description);
                            row++;
                        }

                        worksheet.Cells[worksheet.Dimension.Address].AutoFilter = true;
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        result = File(package.GetAsByteArray(), MediaTypeNames.Application.Octet, $"Log_export_{DateTime.UtcNow}.xlsx");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Export error: " + ex.Message);
            }
            return result;
        }
    }
}

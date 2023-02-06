using LogService2023.App.Models;
using LogService2023.App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IEnumerable<Log>> GetLogs()
        {
            return _logService.List();
        }
    }
}

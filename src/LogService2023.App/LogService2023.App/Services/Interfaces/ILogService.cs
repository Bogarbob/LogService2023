using LogService2023.App.Dtos;
using LogService2023.App.Models;

namespace LogService2023.App.Services.Interfaces
{
    public interface ILogService
    {
        Task<List<Log>> List(LogFilter logFilter);
        Task<Log> Create(CreateLogDto createLogDto);
        Task Delete(DateTimeOffset dateTime);
    }
}

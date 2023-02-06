using LogService2023.App.Models;

namespace LogService2023.App.Services.Interfaces
{
    public interface ILogService
    {
        Task<List<Log>> List();
    }
}

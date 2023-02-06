using LogService2023.App.Enums;
using LogService2023.App.Models;
using LogService2023.App.Services.Interfaces;

namespace LogService2023.App.Services
{
    public class LogService : ILogService
    {
        public List<Log> List()
        {
            return new List<Log>() 
            {
                new() { Id=Guid.NewGuid(), TimeStamp=DateTimeOffset.Now, LogType=LogType.Info, Description="Test Info" },
                new() { Id=Guid.NewGuid(), TimeStamp=DateTimeOffset.Now, LogType=LogType.Warning, Description="Test Warning" },
                new() { Id=Guid.NewGuid(), TimeStamp=DateTimeOffset.Now, LogType=LogType.Error, Description="Test Error" }
            };
        }
    }
}

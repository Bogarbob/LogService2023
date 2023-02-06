using LogService2023.App.Enums;

namespace LogService2023.App.Dtos
{
    public class LogFilter
    {
        public DateTimeOffset? From { get; set; }
        public DateTimeOffset? To { get; set; }
        public LogType? LogType { get; set; }
    }
}

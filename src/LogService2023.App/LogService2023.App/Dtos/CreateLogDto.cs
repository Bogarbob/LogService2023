using LogService2023.App.Enums;

namespace LogService2023.App.Dtos
{
    public class CreateLogDto
    {
        public LogType LogType { get; set; } = LogType.Error;
        public string Description { get; set; }
    }
}

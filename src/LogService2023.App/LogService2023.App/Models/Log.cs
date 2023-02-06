using LogService2023.App.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogService2023.App.Models
{
    [Table("Logs")]
    public class Log
    {
        public Guid Id { get; set; }
        public LogType LogType { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Description { get; set; }
    }
}

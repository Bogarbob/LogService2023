using LogService2023.App.DbContexts;
using LogService2023.App.Enums;
using LogService2023.App.Models;
using LogService2023.App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogService2023.App.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _db;

        public LogService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Log>> List()
        {
            return await _db.Logs.AsNoTracking().ToListAsync();
        }
    }
}

using AutoMapper;
using LogService2023.App.DbContexts;
using LogService2023.App.Dtos;
using LogService2023.App.Models;
using LogService2023.App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogService2023.App.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public LogService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<Log>> List(LogFilter logFilter)
        {
            var logs = _db.Logs.AsNoTracking();

            if(logFilter.LogType.HasValue)
                logs = logs.Where(x => x.LogType == logFilter.LogType);

            if (logFilter.From.HasValue)
                logs = logs.Where(x => x.TimeStamp > logFilter.From);

            if (logFilter.To.HasValue)
                logs = logs.Where(x => x.TimeStamp < logFilter.To);

            return await logs.ToListAsync();
        }

        public async Task<Log> Create(CreateLogDto createLogDto)
        {
            var log = _mapper.Map<Log>(createLogDto);
            await _db.Logs.AddAsync(log);
            await _db.SaveChangesAsync();
            return log;
        }

        public async Task Delete(DateTimeOffset dateTime)
        {
            var logs = _db.Logs.Where(x => x.TimeStamp < dateTime);
            
            _db.RemoveRange(logs);
            await _db.SaveChangesAsync();
        }

    }
}

using LogService2023.App.DbContexts;
using LogService2023.App.Services;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xunit;
using LogService2023.App.Mapping;
using LogService2023.App.Dtos;
using AutoFixture;
using LogService2023.App.Models;
using System.Threading.Tasks;
using LogService2023.App.Enums;

namespace LogService2023.App.Tests
{
    public class LogServiceTests
    {
        public static ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyyMMddHHmmssfff"))
                .Options;

            return new ApplicationDbContext(options);
        }

        public static IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            return new Mapper(configuration);
        }

        [Fact]
        public async Task List_With_Empty()
        {
            var context = GetDbContext();
            var service = new LogService(context, GetMapper());

            var list = await service.List(new LogFilter());
            Assert.Empty(list);
        }

        [Fact]
        public async Task List_With_Many()
        {
            var count = 3;

            var context = GetDbContext();
            var service = new LogService(context, GetMapper());

            var fixture = new Fixture();
            var logList = fixture.Build<Log>().CreateMany(count);

            await context.Logs.AddRangeAsync(logList);
            await context.SaveChangesAsync();

            var list = await service.List(new LogFilter());
            Assert.Equal(count, list.Count);
        }

        [Fact]
        public async Task List_With_TypeFilter()
        {
            var context = GetDbContext();
            var service = new LogService(context, GetMapper());

            var count = 3;
            var fixture = new Fixture();
            var warningLogList = fixture.Build<Log>().With(p => p.LogType, LogType.Warning).CreateMany(1);
            var errorLogList = fixture.Build<Log>().With(p => p.LogType, LogType.Error).CreateMany(1);
            var infoLogList = fixture.Build<Log>().With(p => p.LogType, LogType.Info).CreateMany(count);

            await context.Logs.AddRangeAsync(warningLogList);
            await context.Logs.AddRangeAsync(errorLogList);
            await context.Logs.AddRangeAsync(infoLogList);

            await context.SaveChangesAsync();

            var list = await service.List(new LogFilter(){ LogType = LogType.Info });
            Assert.Equal(count, list.Count);
        }


        [Fact]
        public async Task Delete_With_Filter_Before_Date()
        {
            var context = GetDbContext();
            var service = new LogService(context, GetMapper());

            var dateTime = DateTime.Now;
            var log = new Log() { LogType = LogType.Info, TimeStamp = dateTime, Description= "Description" };

            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();

            await service.Delete(dateTime.AddDays(-1));
            var list = await context.Logs.ToListAsync();
            Assert.NotEmpty(list);

        }

        [Fact]
        public async Task Delete_With_Filter_After_Date()
        {
            var context = GetDbContext();
            var service = new LogService(context, GetMapper());

            var dateTime = DateTime.Now;
            var log = new Log() { LogType = LogType.Info, TimeStamp = dateTime, Description = "Description" };

            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();

            await service.Delete(dateTime.AddDays(1));
            var list = await context.Logs.ToListAsync();
            Assert.Empty(list);
        }


        [Fact]
        public async Task Create()
        {
            var context = GetDbContext();
            var service = new LogService(context, GetMapper());

            var type = LogType.Warning;
            var description = "Test Description";
            var createLogDto = new CreateLogDto() { LogType = type, Description = description };

            await service.Create(createLogDto);

            var log = await context.Logs.FirstAsync();
            Assert.Equal(type, log.LogType);
            Assert.Equal(description, log.Description);
        }
    }
}
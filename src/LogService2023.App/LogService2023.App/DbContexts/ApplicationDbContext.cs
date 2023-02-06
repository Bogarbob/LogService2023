using LogService2023.App.Models;
using Microsoft.EntityFrameworkCore;

namespace LogService2023.App.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Log> Logs { get; set; }

    }
}

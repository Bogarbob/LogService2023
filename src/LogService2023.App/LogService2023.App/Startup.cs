using LogService2023.App.DbContexts;
using LogService2023.App.Services;
using LogService2023.App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

namespace LogService2023.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly Settings settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            settings = new Settings(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(p => settings);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(settings.DbConnectionString));

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ILogService, LogService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}

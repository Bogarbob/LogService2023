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

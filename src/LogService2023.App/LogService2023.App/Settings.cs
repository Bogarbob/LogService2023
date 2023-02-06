namespace LogService2023.App
{
    public class Settings
    {
        public string DbConnectionString { get; }

        public Settings(IConfiguration config)
        {
            DbConnectionString = config["ConnectionStrings:DatabaseConnection"];
        }
    }
}
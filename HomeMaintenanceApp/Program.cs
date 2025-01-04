using Serilog;

namespace homeMaintenanceApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Information("Application starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "app failed to start");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((config) =>
            {
                var buildConfig = config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddCommandLine(args)
                .Build();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}

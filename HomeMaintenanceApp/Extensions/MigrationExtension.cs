using DbUp;
using homeMaintenance.Application.Ports.In.Config;

namespace HomeMaintenanceApp.Web.Extensions
{
    public static partial class MigrationExtension
    {

        public static void RunApplicationMigrations(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var dbMigration = provider.GetRequiredService<IDbConfig>();
                var baseDirectory = @"C:\HomeMaintenance\API\homeMaintenance.Infrastructure";
                var scriptsFolder = "Scripts";
                var scriptsPath = Path.Combine(baseDirectory, scriptsFolder);

                EnsureDatabase.For.SqlDatabase(dbMigration.GetConnection().ConnectionString);

                var upgrader = DeployChanges.To
                                .SqlDatabase(dbMigration.GetConnection().ConnectionString)
                                .WithScriptsFromFileSystem(scriptsPath)
                                .LogToConsole()
                                .Build();

                if (upgrader.IsUpgradeRequired())
                {
                    var result = upgrader.PerformUpgrade();
                    if (!result.Successful)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: {0}", result.Error);
                        Console.ResetColor();
                        Environment.ExitCode = -1;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Database migrations applied successfully!");
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}

using DbUp;

namespace HomeMaintenanceApp.Web.Extensions
{
    public static partial class MigrationExtension
    {

        public static void RunApplicationMigrations(this IApplicationBuilder app)
        {
            // vidi zoshto se pravi ova create scope
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var dbMigration = provider.GetRequiredService<IDbConfig>();
                var baseDirectory = @"C:\HomeMaintenance\API\homeMaintenance.Infrastructure";
                var scriptsFolder = "Scripts";
                var scriptsPath = Path.Combine(baseDirectory, scriptsFolder);

                EnsureDatabase.For.SqlDatabase(dbMigration.GetConnectionString());

                var upgrader = DeployChanges.To
                                .SqlDatabase(dbMigration.GetConnectionString())
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

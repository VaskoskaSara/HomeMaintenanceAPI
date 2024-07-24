using System.Data;
using System.Data.Common;
using System.Reflection;

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
                var scriptsPath = !string.IsNullOrWhiteSpace(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) ? Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts") : "Scripts";

                var evolve = new Evolve.Evolve((DbConnection)dbMigration.GetConnection())
                {
                    Locations = new[] { "C:\\Users\\user\\Desktop\\HomeMaintenance\\HomeMaintenanceApp\\homeMaintenance.Infrastructure\\Scripts" },
                    IsEraseDisabled = false,
                    OutOfOrder = true,
                    TransactionMode = Evolve.Configuration.TransactionKind.CommitAll
                };
                evolve.Repair();
                evolve.Migrate();
            }
        }
    }
}

using Dapper;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Infrastructure.Adapters.Db;
using System.Data;

namespace homeMaintenance.Infrastructure.Adapters.Repositories
{
    public class PositionRepository
    {
        private readonly IDbHelper _dbHelper;

        public PositionRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task SeedPositionsAsync()
        {
            try
            {
                var existingPositions = await GetExistingPositionsAsync();

                var predefinedPositions = GetPredefinedPositions();
                var newPositions = predefinedPositions.Except(existingPositions).ToList();

                if (newPositions.Any())
                {
                    await InsertNewPositionsAsync(newPositions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while seeding positions: {ex.Message}");
                throw;
            }
        }

        private async Task<List<string>> GetExistingPositionsAsync()
        {
            var positions = await _dbHelper.ExecuteQueryAsync<Position>("GetPositions");

            return positions.Select(p => p.PositionName).ToList();
        }

        private List<string> GetPredefinedPositions()
        {
            return new List<string>
            {
                "Home Service Technician", "Residential Maintenance Worker", "Housekeeping Specialist",
                "Property Manager", "Plumber", "Electrician", "HVAC Technician", "Landscaper", "Painter",
                "Handyman", "Carpenter", "Pest Control Technician", "Security System Installer",
                "Pool Maintenance Technician", "Cleaning Supervisor", "Home Appliance Repair Technician",
                "Window Washer", "Roofing Specialist", "General Contractor"
            };
        }

        private async Task InsertNewPositionsAsync(List<string> newPositions)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("PositionName", typeof(string));

            foreach (var position in newPositions)
            {
                dataTable.Rows.Add(position);
            }

            var parameters = new { PositionNames = dataTable.AsTableValuedParameter("dbo.PositionNameTableType") };

            await _dbHelper.ExecuteAsync("InsertPositionsBulk", parameters);
        }
    }
}

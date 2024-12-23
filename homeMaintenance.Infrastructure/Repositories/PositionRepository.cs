using Dapper;
using homeMaintenance.Domain.Entities;
using HomeMaintenanceApp.Web;
using System.Data;

namespace homeMaintenance.Infrastructure.Repositories
{
    public class PositionRepository
    {
        private readonly IDbConnection _dbConnection;

        public PositionRepository(IDbConfig dbConfig)
        {
            _dbConnection = dbConfig.GetConnection();
        }
        public async Task SeedPositions()
        {
            var existingPositions = await _dbConnection.QueryAsync<Position>("GetPositions", commandType: CommandType.StoredProcedure);
            var positions = new[]
            {
    "Home Service Technician",
    "Residential Maintenance Worker",
    "Housekeeping Specialist",
    "Property Manager",
    "Plumber",
    "Electrician",
    "HVAC Technician",
    "Landscaper",
    "Painter",
    "Handyman",
    "Carpenter",
    "Pest Control Technician",
    "Security System Installer",
    "Pool Maintenance Technician",
    "Cleaning Supervisor",
    "Home Appliance Repair Technician",
    "Window Washer",
    "Roofing Specialist",
    "General Contractor"
};

            var existingPositionNames = existingPositions.Select(x => x.PositionName).ToList();

            var newPositions = positions.Where(p => !existingPositionNames.Contains(p)).ToList();

            if (newPositions.Any())
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("PositionName", typeof(string));

                foreach (var position in newPositions)
                {
                    dataTable.Rows.Add(position);
                }

                var parameters = new { PositionNames = dataTable.AsTableValuedParameter("dbo.PositionNameTableType") };

                await _dbConnection.ExecuteAsync("InsertPositionsBulk", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}

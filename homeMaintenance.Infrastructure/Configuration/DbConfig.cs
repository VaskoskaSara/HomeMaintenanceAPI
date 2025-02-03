using homeMaintenance.Application.Ports.In.Config;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace homeMaintenance.Infrastructure.Configuration
{
    public class DbConfig : IDbConfig
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _dbConfigSection;

        public DbConfig(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConfigSection = _configuration.GetSection("DbConnection");
        }

        public string DatabaseName => _dbConfigSection["DatabaseName"];
        public string ServerName => _dbConfigSection["sqlServerName"];
        public string Username => _dbConfigSection["sqlAdminUsername"];
        public string Password => _dbConfigSection["sqlAdminPassword"];
        public int ConnectionTimeout => int.Parse(_dbConfigSection["Connection Timeout"]);

        public IDbConnection GetConnection()
        {
            var sqlConnectionString = new SqlConnectionStringBuilder()
            {
                PersistSecurityInfo = true,
                DataSource = ServerName,
                UserID = Username,
                Password = Password,
                InitialCatalog = DatabaseName,
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                MaxPoolSize = 500,
                ConnectTimeout = ConnectionTimeout
            };

            return new SqlConnection(sqlConnectionString.ConnectionString);
        }

        public string GetConnectionString()
        {
            var sqlConnectionString = new SqlConnectionStringBuilder()
            {
                PersistSecurityInfo = true,
                DataSource = ServerName,
                UserID = Username,
                Password = Password,
                InitialCatalog = DatabaseName,
                TrustServerCertificate = true,
                MaxPoolSize = 500,
                ConnectTimeout = ConnectionTimeout
            };

            return sqlConnectionString.ConnectionString;
        }
    }
}
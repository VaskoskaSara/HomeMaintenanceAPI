using Amazon.S3;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HomeMaintenanceApp.Web
{
    public class DbConfig : IDbConfig
    {
        public readonly IConfiguration _configuration;
        public readonly IConfigurationSection _configurationSection;

        public DbConfig(IConfiguration configuration)
        {
            _configuration = configuration;
            _configurationSection = _configuration.GetSection("DbConnection");
        }


        public string DatabaseName => _configurationSection["DatabaseName"];

        public string ServerName => _configurationSection["sqlServerName"];

        public string Username => _configurationSection["sqlAdminUsername"];

        public string Password => _configurationSection["sqlAdminPassword"];

        public int ConnectionTimeout => int.Parse(_configurationSection["Connection Timeout"]);

        // public string Configuration => _configurationSection["Configuration"];

        public string AwsAccessKey => _configuration["AwsConfiguration:AWSAccessKey"];
        public string AwsSecretKey => _configuration["AwsConfiguration:AWSSecretKey"];

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
                // MultipleActiveResultSets = true,
                MaxPoolSize = 500,
                ConnectTimeout = ConnectionTimeout
            };

            return sqlConnectionString.ConnectionString;
        }


        public AmazonS3Client GetAwsClient()
        {
            var client = new AmazonS3Client(AwsAccessKey, AwsSecretKey, Amazon.RegionEndpoint.EUCentral1);

            return client;
        }
    }
}

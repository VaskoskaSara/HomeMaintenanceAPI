using Amazon.S3;
using System.Data;
using System.Data.Common;

namespace HomeMaintenanceApp.Web
{
    public interface IDbConfig
    {
        IDbConnection GetConnection();

        string GetConnectionString();
        AmazonS3Client GetAwsClient();
    }
}

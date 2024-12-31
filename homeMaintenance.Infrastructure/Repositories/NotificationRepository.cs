using Dapper;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using System.Data;

namespace homeMaintenance.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbConnection _dbConnection;

        public NotificationRepository(IDbConfig dbConfig)
        {
            _dbConnection = dbConfig.GetConnection();
        }
        public async Task<IEnumerable<NotificationResult>> GetUnsentNotifications(Guid userId)
        {
            var notifications = await _dbConnection.QueryAsync<NotificationResult>("GetUnsendNotifications", new
            {
                UserId = userId
            },
            commandType: CommandType.StoredProcedure);

            return notifications;
        }

        public async Task MarkNotificationAsSent(int id)
        {

            var notifications = await _dbConnection.QueryAsync<NotificationResult>("UpdateNotificationReviews",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);
        }
    }
}

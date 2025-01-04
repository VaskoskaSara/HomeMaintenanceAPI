using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Infrastructure.Adapters.Db;

namespace homeMaintenance.Infrastructure.Adapters.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbHelper _dbHelper;

        public NotificationRepository(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<IEnumerable<NotificationResult>> GetUnsentNotifications(Guid userId)
        {
            return await _dbHelper.ExecuteQueryAsync<NotificationResult>("GetUnsendNotifications", new
            {
                UserId = userId
            });
        }

        public async Task MarkNotificationAsSent(int id)
        {

            await _dbHelper.ExecuteQueryAsync<NotificationResult>("UpdateNotificationReviews",
            new
            {
                Id = id
            });
        }
    }
}

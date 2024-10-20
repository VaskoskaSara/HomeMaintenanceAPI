using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.Out
{
    public interface INotificationRepository
    {
        Task MarkNotificationAsSent(int id);
        Task<IEnumerable<NotificationResult>> GetUnsentNotifications(Guid userId);
    }
}

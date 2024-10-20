using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.In
{
    public interface INotificationService
    {
        Task<IList<NotificationDto>> GetUnsendNotifications(Guid userId);
    }
}

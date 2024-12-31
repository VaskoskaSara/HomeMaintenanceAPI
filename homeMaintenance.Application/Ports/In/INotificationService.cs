using homeMaintenance.Application.DTOs;

namespace homeMaintenance.Application.Ports.In
{
    public interface INotificationService
    {
        Task<IList<NotificationDto>> GetUnsendNotifications(Guid userId);
    }
}

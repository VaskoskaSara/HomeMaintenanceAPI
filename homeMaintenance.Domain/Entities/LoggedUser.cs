using homeMaintenance.Domain.Enum;

namespace homeMaintenance.Domain.Entities
{
    public class LoggedUser
    {
        public Guid Id { get; set; }
        public UserType UserRole { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<NotificationDto> notifications { get; set; } = Enumerable.Empty<NotificationDto>();
    }
}

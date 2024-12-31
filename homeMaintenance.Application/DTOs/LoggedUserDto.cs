using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;

namespace homeMaintenance.Application.DTOs
{
    public class LoggedUserDto
    {
        public Guid Id { get; set; }
        public UserType UserRole { get; set; }
        public string Avatar { get; set; }
        public IEnumerable<NotificationDto> notifications { get; set; } = Enumerable.Empty<NotificationDto>();
    }
}

using homeMaintenance.Domain.Enum;

namespace homeMaintenance.Domain.Entities
{
    public class UserLogin
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserRole { get; set; }
        public string Avatar { get; set; }
    }
}

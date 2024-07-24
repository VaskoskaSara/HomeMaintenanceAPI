using homeMaintenance.Domain.Enum;

namespace homeMaintenance.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public UserType? UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public float Experience { get; set; }
        public string PositionId { get; set; }
        public float Price { get; set; }

    }
}

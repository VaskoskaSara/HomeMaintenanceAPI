using homeMaintenance.Domain.Enum;

namespace homeMaintenance.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public UserType? UserType { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public float? Experience { get; set; }
        public Guid? PositionId { get; set; }
        public string? NewPosition { get; set; }
        public PaymentType? PaymentType { get; set; }
        public float? Price { get; set; }
        public string Avatar { get; set; }
        public string[]? Photos { get; set; }
    }
}

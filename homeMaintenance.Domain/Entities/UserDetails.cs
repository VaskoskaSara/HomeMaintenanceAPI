namespace homeMaintenance.Domain.Entities
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string RoleName { get; set; }
        public float? Experience { get; set; }
        public float? Price { get; set; }
        public string PaymentType { get; set; }
        public string Avatar { get; set; }
        public string PositionName { get; set; }
        public DateTime BirthDate { get; set; }
        public int? NumberOfEmployees { get; set; }
        public string? Description { get; set; }
        public List<string> Photos { get; set; } = new List<string>();
        public RatingObject Rating { get; set; }

    }
}

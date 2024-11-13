namespace homeMaintenance.Domain.Entities
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public float Experience { get; set; }
        public Guid PositionId { get; set; }
        public float? Price { get; set; }
        public string Avatar { get; set; }
        public RatingObject Rating { get; set; }
    }
}

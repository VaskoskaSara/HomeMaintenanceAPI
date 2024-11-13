namespace homeMaintenance.Domain.Entities
{
    public class UserReviews
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string? Avatar { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public string? Photo { get; set; }
        public string? PaymentId { get; set; }
    }
}

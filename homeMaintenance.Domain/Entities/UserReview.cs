namespace homeMaintenance.Domain.Entities
{
    public class UserReview
    {
        public string? PaymentId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid UserId { get; set; }
        public int? Rating { get; set; }
        public string[]? Photos { get; set; }
        public string? Comment { get; set; }
        public Guid UserPaymentId { get; set; }
    }
}

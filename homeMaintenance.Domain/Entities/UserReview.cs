namespace homeMaintenance.Domain.Entities
{
    public class UserReview
    {
        public object Photo;

        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string? PaymentId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public string FullName { get; set; }
        public string? Avatar { get; set; }
        public List<string>? Photos { get; set; }
        public Guid? UserPaymentId { get; set; }
    }
}

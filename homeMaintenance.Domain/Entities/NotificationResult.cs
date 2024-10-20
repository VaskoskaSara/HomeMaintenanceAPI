namespace homeMaintenance.Domain.Entities
{
    public class NotificationResult
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string PaymentId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

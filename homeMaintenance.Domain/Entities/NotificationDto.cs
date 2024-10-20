namespace homeMaintenance.Domain.Entities
{
    public class NotificationDto
    {
        public string PaymentId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}

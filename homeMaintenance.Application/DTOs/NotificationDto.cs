namespace homeMaintenance.Application.DTOs
{
    public class NotificationDto
    {
        public string PaymentId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid UserPaymentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

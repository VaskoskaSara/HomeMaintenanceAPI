namespace homeMaintenance.Application.DTOs
{
    public class NotificationDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid UserPaymentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

namespace homeMaintenance.Application.DTOs
{
    public class BookingInfoDto
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public long Amount { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsEmployeeReviewed { get; set; }
        public Guid EmployeeId { get; set; }
        public string? PaymentId { get; set; }
        public Guid UserPaymentId { get; set; }
        public string Address { get; set; }
    }
}

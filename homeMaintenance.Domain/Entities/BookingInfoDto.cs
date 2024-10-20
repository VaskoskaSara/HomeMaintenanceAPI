namespace homeMaintenance.Domain.Entities
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
    }
}

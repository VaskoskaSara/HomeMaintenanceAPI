namespace homeMaintenance.Application.DTOs.Requests
{
    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string? PaymentMethodId { get; set; }
    }
}

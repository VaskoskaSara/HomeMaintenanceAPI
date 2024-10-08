namespace homeMaintenance.Domain.Entities
{
    public class PaymentRequest
    {
        public long Amount { get; set; } 
        public string? PaymentMethodId { get; set; } 
    }
}

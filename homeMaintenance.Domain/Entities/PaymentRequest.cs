namespace homeMaintenance.Domain.Entities
{
    public class PaymentRequest
    {
        public long Amount { get; set; } // Amount in cents
        public string PaymentMethodId { get; set; } // Stripe Payment Method ID
    }
}

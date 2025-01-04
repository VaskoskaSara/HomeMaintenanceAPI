using MediatR;

namespace homeMaintenance.Application.Commands.TransactionInformation
{
    public class TransactionInfoCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public long Amount { get; set; }
        public string? PaymentId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}

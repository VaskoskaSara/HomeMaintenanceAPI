using MediatR;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public record EmployeeReviewCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string? PaymentId { get; set; }
        public Guid EmployeeId { get; set; }
        public int? Rating { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public string? Comment { get; set; }
        public Guid UserPaymentId { get; set; }
    }
}

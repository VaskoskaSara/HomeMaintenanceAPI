using MediatR;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Commands.EmployeeReview
{
 public record EmployeeReviewCommand(
        Guid UserId,
        string? PaymentId,
        Guid EmployeeId,
        int? Rating,
        List<IFormFile>? Photos,
        string? Comment,
        Guid UserPaymentId
    ) : IRequest<bool>;
}

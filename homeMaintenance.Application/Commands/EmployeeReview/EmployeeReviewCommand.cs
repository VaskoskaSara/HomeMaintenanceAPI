using MediatR;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Commands.EmployeeReview
{
 public record EmployeeReviewCommand(
        Guid UserId,
        Guid PaymentId,
        Guid EmployeeId,
        int? Rating,
        List<IFormFile>? Photos,
        string? Comment
    ) : IRequest<bool>;
}

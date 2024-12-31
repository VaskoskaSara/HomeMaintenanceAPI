using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record ReviewsByUser(Guid id) : IRequest<List<UserReviewsDto>>;
}

using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record ReviewsByUser(Guid id) : IRequest<List<UserReviewsDto>>;
}

using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetReviewsByUser
{
    public record ReviewsByUser(Guid id) : IRequest<List<UserReviewsDto>>;
}

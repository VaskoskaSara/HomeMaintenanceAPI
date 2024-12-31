using homeMaintenance.Application.DTOs;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Ports.In
{
    public interface IReviewService
    {
        Task<bool> AddReview(AddUserReview user, CancellationToken cancellationToken = default);
        Task<List<UserReviewsDto?>> GetReviewsByUser(Guid id, CancellationToken cancellationToken = default);
    }
}

using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetReviewsByUser
{
    public class ReviewsByUserQueryHandler : IRequestHandler<ReviewsByUser, List<UserReviewsDto>>
    {
        private readonly IReviewService _reviewService;
        public ReviewsByUserQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<List<UserReviewsDto>> Handle(ReviewsByUser request, CancellationToken cancellationToken)
        {
            var response = await _reviewService.GetReviewsByUser(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}

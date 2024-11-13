using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class ReviewsByUserQueryHandler : IRequestHandler<ReviewsByUser, List<UserReviewsDto>>
    {
        private readonly IServiceContainer _serviceContainer;
        public ReviewsByUserQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<List<UserReviewsDto>> Handle(ReviewsByUser request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetReviewsByUser(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}

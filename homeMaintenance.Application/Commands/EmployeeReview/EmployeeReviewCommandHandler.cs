using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.EmployeeReview
{
    public class EmployeeReviewCommandHandler : IRequestHandler<EmployeeReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;

        public EmployeeReviewCommandHandler(IReviewService reviewService, IImageStorageService imageStorageService, IMapper mapper)
        {
            _reviewService = reviewService;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EmployeeReviewCommand request, CancellationToken cancellationToken)
        {
            var userReview = _mapper.Map<AddUserReview>(request);

            var result = await _reviewService.AddReview(userReview, cancellationToken).ConfigureAwait(false);

            if (!result) return false;

            if (request.Photos != null)
            {
                var uploadTasks = request.Photos.Select(_imageStorageService.UploadImageAsync).ToArray();
                await Task.WhenAll(uploadTasks);
            }

            return true;
        }
    }
}


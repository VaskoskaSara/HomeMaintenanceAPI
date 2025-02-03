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

            if (request.Photos != null)
            {
                userReview.Photos = new string[request.Photos.Count];
                for (int i = 0; i < request.Photos.Count; i++)
                {
                    string imageName = GenerateFileNameWithGuid(request.Photos[i].FileName);
                    userReview.Photos[i] = imageName;
                    await _imageStorageService.UploadImageAsync(request.Photos[i], imageName);
                }
            }

            var result = await _reviewService.AddReview(userReview, cancellationToken).ConfigureAwait(false);

            if (!result) return false;

            return true;
        }

        private static string GenerateFileNameWithGuid(string originalFileName)
        {
            return $"{Guid.NewGuid()}_{originalFileName}";
        }
    }
}


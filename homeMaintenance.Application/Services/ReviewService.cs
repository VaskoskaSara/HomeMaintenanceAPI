using AutoMapper;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;


        public ReviewService(IUserRepository userRepository, IImageStorageService imageStorageService, IMapper mapper)
        {
            _userRepository = userRepository;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }
        public async Task<bool> AddReview(AddUserReview user, CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.AddReview(user);

            return result;
        }

        public async Task<List<UserReviewsDto?>> GetReviewsByUser(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetReviewsByUserAsync(id);

            var groupedReviews = response
                .GroupBy(r => new { r.UserId, r.PaymentId, r.Avatar, r.FullName, r.Comment, r.Rating })
                .Select(g => new UserReviewsDto
                {
                    UserId = g.Key.UserId,
                    FullName = g.Key.FullName,
                    Avatar = g.Key.Avatar,
                    Comment = g.Key.Comment,
                    Rating = (int)g.Key.Rating,
                    Photos = g.Any(x => x.Photo != null) ? g.Select(x => x.Photo as string).ToList() : null
                }).ToList();

            foreach (var item in groupedReviews)
            {
                item.Avatar = _imageStorageService.GetImagePath(item.Avatar);

                item.Photos = item.Photos?.Select(_imageStorageService.GetImagePath).ToList() ?? new List<string>();
            }

            return groupedReviews;
        }

    }
}

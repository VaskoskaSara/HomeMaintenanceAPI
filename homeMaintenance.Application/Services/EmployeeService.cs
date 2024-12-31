using AutoMapper;
using homeMaintenance.Application.Interfaces;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Application.DTOs;

namespace homeMaintenance.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;


        public EmployeeService(IUserRepository userRepository, IImageStorageService imageStorageService, IMapper mapper)
        {
            _userRepository = userRepository;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees(string[]? cities, int? price, int? experience, bool? excludeByContract, Guid[] categoryIds, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetEmployeesAsync(cities, price, experience, excludeByContract, categoryIds);
            var employees = _mapper.Map<IEnumerable<EmployeeDto>>(response);

            foreach (var item in employees)
            {
                var ratings = await _userRepository.GetRatingByEmployeeId(item.Id);
                item.Rating = CalculateRating(ratings);
            }

            foreach (var employee in employees)
            {
                employee.Avatar = _imageStorageService.GetImagePath(employee.Avatar);
            }

            return employees;
        }
        public async Task<UserDetailsDto?> GetEmployeeById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetEmployeeByIdAsync(id);
            var userDetails = _mapper.Map<UserDetailsDto>(response);

            var ratings = await _userRepository.GetRatingByEmployeeId(id);
            userDetails.Rating = CalculateRating(ratings);

            userDetails.Avatar = _imageStorageService.GetImagePath(userDetails.Avatar);
            userDetails.Photos = userDetails.Photos?.Select(_imageStorageService.GetImagePath).ToList();

            return userDetails;
        }

        private static RatingObject CalculateRating(IEnumerable<int> ratings)
        {
            var numberOfReviews = ratings.Count();
            var rating = numberOfReviews > 0
                ? (double)ratings.Sum() / numberOfReviews
                : 0.0;

            return new RatingObject
            {
                NumberOfReviews = numberOfReviews,
                Rating = rating
            };
        }

        public async Task<List<DateOnly>> GetDisabledDatesByEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetDisabledDatesByEmployeeAsync(id);
        }

        public async Task<List<DateTime>> GetBookedDatesByEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var bookings = await _userRepository.GetBookingsByEmployeeAsync(id);

            var allBookingDates = bookings.Select(c => (c.StartDateTime, c.EndDateTime))
                        .SelectMany(b => Enumerable.Range(0, (b.EndDateTime - b.StartDateTime).Days + 1).Select(d => b.StartDateTime.AddDays(d)))
                        .Distinct()
                        .ToList();

            return allBookingDates;
        }

        public async Task<IEnumerable<BookingInfoDto?>> GetBookingsByEmployee(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetBookingsByEmployeeAsync(id);

            foreach (var item in response)
            {
                item.Avatar = _imageStorageService.GetImagePath(item.Avatar);
            }

            return _mapper.Map<IEnumerable<BookingInfoDto>>(response);
        }
    }
}

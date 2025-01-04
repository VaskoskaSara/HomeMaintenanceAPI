using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Application.DTOs;
using AutoMapper;

namespace homeMaintenance.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IMapper mapper, INotificationService notificationService, IImageStorageService s3Service, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _imageStorageService = s3Service;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<Position>?> GetPositions(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetPositionsAsync();
        }
        public async Task<IEnumerable<string>?> GetCities(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetCitiesAsync();
        }
        public async Task<bool> PostAvaliabilty(EmployeeDisableDates employeeDisableDates, CancellationToken cancellationToken = default)
        {
            return await _userRepository.PostAvaliability(employeeDisableDates);
        }
        public async Task<IEnumerable<BookingInfoDto?>> GetBookingsByUser(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _userRepository.GetBookingsByUserAsync(id);

            foreach (var item in response)
            {
                item.Avatar = _imageStorageService.GetImagePath(item.Avatar);
            }

            return _mapper.Map<IEnumerable<BookingInfoDto>>(response);
        }
    }
}

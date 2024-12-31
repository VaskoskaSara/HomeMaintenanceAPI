using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Interfaces;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;

namespace homeMaintenance.Application.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageStorageService _imageStorageService;
        private readonly INotificationService _notificationService;
        
        public UserAuthenticationService(
            IUserRepository userRepository,
            IImageStorageService imageStorageService,
            INotificationService notificationService)
        {
            _userRepository = userRepository;
            _imageStorageService = imageStorageService;
            _notificationService = notificationService;
        }

        public async Task<LoggedUserDto> AuthenticateUserAsync(User loginUser, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                throw new ArgumentException("Username and password cannot be empty.");
            }

            var user = await _userRepository.GetUserByEmailAsync(loginUser.Email);

            if (user == null)
                throw new ArgumentException("Username and password cannot be empty.");

            var notifications = await _notificationService.GetUnsendNotifications(user.Id);

            if (notifications.Any())
            {
                foreach (var item in notifications)
                {
                    IEnumerable<string> employee = await _userRepository.GetEmployeeNameById(item.EmployeeId);
                    item.EmployeeName = employee.FirstOrDefault();
                }
            }

            return new LoggedUserDto
            {
                Id = user.Id,
                UserRole = user.UserRole,
                Avatar = _imageStorageService.GetImagePath(user.Avatar),
                notifications = notifications
            };
        }
    }
}

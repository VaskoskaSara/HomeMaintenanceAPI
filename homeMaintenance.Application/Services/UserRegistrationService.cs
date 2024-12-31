using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Interfaces;
using homeMaintenance.Application.Ports.Out;
using homeMaintenance.Domain.Entities;
using homeMaintenance.Domain.Enum;
using homeMaintenance.Domain.Exceptions;
using System.Net;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IImageStorageService _s3Service;

    public UserRegistrationService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IImageStorageService s3Service)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _s3Service = s3Service;
    }

    public async Task<LoggedUserDto> RegisterUserAsync(User user, CancellationToken cancellationToken = default)
    {
        ValidatePassword(user.Password);

        if (await _userRepository.GetUserByEmailAsync(user.Email) != null)
            throw new CustomException(HttpStatusCode.Conflict, "User with that email already exists");

        user.Password = await HashPasswordAsync(user.Password);
        await AssignUserPositionAndPriceCheckAsync(user);

        var response = await _userRepository.RegisterUser(user);
        await InsertUserPhotosAsync(response.Id, user.Photos);

        return new LoggedUserDto
        {
            Id = response.Id,
            UserRole = response.UserRole,
            Avatar = GetImagePath(user?.Avatar)
        };
    }

    private void ValidatePassword(string password)
    {
        if (!_passwordHasher.IsValidStrongPassword(password))
            throw new CustomException(HttpStatusCode.BadRequest, "Password is not strong enough");
    }

    private async Task<string> HashPasswordAsync(string password)
    {
        string hashedPassword = _passwordHasher.HashPassword(password, _passwordHasher.GenerateSalt());
        if (string.IsNullOrEmpty(hashedPassword))
            throw new CustomException(HttpStatusCode.InternalServerError, "Error occurred while saving password");

        return hashedPassword;
    }

    private async Task AssignUserPositionAndPriceCheckAsync(User user)
    {
        if (user.UserType == UserType.Customer) return;

        if (user.PositionId == null)
        {
            if (string.IsNullOrEmpty(user.NewPosition))
                throw new CustomException(HttpStatusCode.BadRequest, "Position must be entered");

            await AssignNewPositionToUser(user);
        }

        if (user.PaymentType != PaymentType.excludeByContract && user.Price == null)
            throw new CustomException(HttpStatusCode.BadRequest, "Price must be entered");
    }

    private async Task AssignNewPositionToUser(User user)
    {
        var positions = await _userRepository.GetPositionsAsync();
        if (!positions.Any(p => p.PositionName.Equals(user.NewPosition, StringComparison.OrdinalIgnoreCase)))
        {
            user.PositionId = await _userRepository.InsertPosition(user.NewPosition);
        }
    }

    private async Task InsertUserPhotosAsync(Guid userId, IEnumerable<string>? photos)
    {
        if (photos != null && photos.Any())
        {
            await _userRepository.InsertUserPhotosAsync(photos, userId);
        }
    }

    private string GetImagePath(string? avatarKey)
    {
        return string.IsNullOrEmpty(avatarKey)
            ? _s3Service.GetImageUrl("defaultUser.jpg")
            : _s3Service.GetImageUrl(avatarKey);
    }
}

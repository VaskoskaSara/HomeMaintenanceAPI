using AutoMapper;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, LoggedUserDto?>
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserAuthenticationService _authenticationService;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;

        public UserRegistrationCommandHandler(IUserRegistrationService userRegistrationService, IImageStorageService imageStorageService, IMapper mapper, IUserAuthenticationService authenticationService)
        {
            _userRegistrationService = userRegistrationService;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<LoggedUserDto?> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            if (request.Avatar != null)
            {
                string avatarImageName = GenerateFileNameWithGuid(request.Avatar.FileName);
                user.Avatar = avatarImageName;
                await _imageStorageService.UploadImageAsync(request.Avatar, avatarImageName);
            }

            if (request.Photos != null)
            {
                user.Photos = new string[request.Photos.Count];
                for (int i = 0; i < request.Photos.Count; i++)
                {
                    string photoImageName = GenerateFileNameWithGuid(request.Photos[i].FileName);
                    user.Photos[i] = photoImageName;
                    await _imageStorageService.UploadImageAsync(request.Photos[i], photoImageName);
                }
            }

            var result = await _userRegistrationService.RegisterUserAsync(user, cancellationToken).ConfigureAwait(false);

            return result;
        }

        private static string GenerateFileNameWithGuid(string originalFileName)
        {
            return $"{Guid.NewGuid()}_{originalFileName}";
        }
    }
}


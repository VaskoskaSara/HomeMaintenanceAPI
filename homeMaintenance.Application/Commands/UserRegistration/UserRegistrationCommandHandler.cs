using AutoMapper;
using homeMaintenance.Application.DTOs;
using homeMaintenance.Application.Interfaces;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, LoggedUserDto?>
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IImageStorageService _imageStorageService;
        private readonly IMapper _mapper;

        public UserRegistrationCommandHandler(IUserRegistrationService userRegistrationService, IImageStorageService imageStorageService, IMapper mapper)
        {
            _userRegistrationService = userRegistrationService;
            _imageStorageService = imageStorageService;
            _mapper = mapper;
        }

        public async Task<LoggedUserDto?> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            
            var result = await _userRegistrationService.RegisterUserAsync(user, cancellationToken).ConfigureAwait(false);

            if (request.Avatar != null)
            {
                await _imageStorageService.UploadImageAsync(request.Avatar);
            }

            if (request.Photos != null)
            {
                request.Photos.ForEach(async photo =>
                {
                    await _imageStorageService.UploadImageAsync(photo);
                });
            }

            return result;
        }
    }
}


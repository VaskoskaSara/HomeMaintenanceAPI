using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Guid?>
    {
        private readonly IServiceContainer _serviceContainer;
        private readonly IMapper _mapper;

        public UserRegistrationCommandHandler(IServiceContainer serviceContainer, IMapper mapper)
        {
            _serviceContainer = serviceContainer;
            _mapper = mapper;
        }

        public async Task<Guid?> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            
            var result = await _serviceContainer.UserService.RegistrationAsync(user, cancellationToken).ConfigureAwait(false);

            if (request.Avatar != null)
            {
                await _serviceContainer.UserService.UploadImageToS3(request.Avatar);
            }

            if (request.Photos != null)
            {
                request.Photos.ForEach(async photo =>
                {
                    await _serviceContainer.UserService.UploadImageToS3(photo);
                });
            }

            return result;
        }
    }
}


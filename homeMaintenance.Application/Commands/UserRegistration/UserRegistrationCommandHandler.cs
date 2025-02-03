﻿using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, long?>
    {
        private readonly IServiceContainer _serviceContainer;
        private readonly IMapper _mapper;

        public UserRegistrationCommandHandler(IServiceContainer serviceContainer, IMapper mapper)
        {
            _serviceContainer = serviceContainer;
            _mapper = mapper;
        }

        public async Task<long?> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            if (request.Avatar != null) {
                await _serviceContainer.UserService.UploadImageToS3(request.Avatar);
            }

            var result = await _serviceContainer.UserService.RegistrationAsync(user, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}


using AutoMapper;
using homeMaintenance.Application.Ports.In;
using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Commands.UserRegistration
{
    public class EmployeeReviewCommandHandler : IRequestHandler<EmployeeReviewCommand, bool>
    {
        private readonly IServiceContainer _serviceContainer;
        private readonly IMapper _mapper;

        public EmployeeReviewCommandHandler(IServiceContainer serviceContainer, IMapper mapper)
        {
            _serviceContainer = serviceContainer;
            _mapper = mapper;
        }

        public async Task<bool> Handle(EmployeeReviewCommand request, CancellationToken cancellationToken)
        {
            var userReview = _mapper.Map<UserReview>(request);
            
            var result = await _serviceContainer.UserService.AddReview(userReview, cancellationToken).ConfigureAwait(false);

            if (request.Photos != null)
            {
                request.Photos.ForEach(async photo =>
                {
                    await _serviceContainer.UserService.UploadImageToS3(photo);
                });
            }

            return true;
        }
    }
}


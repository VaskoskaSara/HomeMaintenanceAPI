using homeMaintenance.Application.Ports.In;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public class BookedDatesByEmployeeQueryHandler : IRequestHandler<BookedDatesByEmployee, List<DateTime>>
    {
        private readonly IServiceContainer _serviceContainer;
        public BookedDatesByEmployeeQueryHandler(IServiceContainer serviceContainer) {
            _serviceContainer = serviceContainer;
        }

        public async Task<List<DateTime>> Handle(BookedDatesByEmployee request, CancellationToken cancellationToken)
        {
            var response = await _serviceContainer.UserService.GetBookedDatesByEmployee(request.id, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}

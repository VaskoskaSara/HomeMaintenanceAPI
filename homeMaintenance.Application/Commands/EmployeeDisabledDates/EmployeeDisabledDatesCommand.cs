using MediatR;

namespace homeMaintenance.Application.Commands.EmployeeDisabledDates
{
    public record EmployeeDisabledDates(
           Guid UserId,
           DateTime[] DisabledDates,
           bool? IsEnabled
       ) : IRequest<bool>;
}

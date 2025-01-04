using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetEmployee
{
    public record EmployeeQuery(Guid id) : IRequest<UserDetailsDto>;
}

using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record EmployeeQuery(Guid id) : IRequest<UserDetailsDto>;
}

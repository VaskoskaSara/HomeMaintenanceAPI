using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record EmployeeQuery(Guid id) : IRequest<UserDetails>;
}

using homeMaintenance.Domain.Entities;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record EmployeesQuery(string city, int? price,int? experience, bool? byContract) : IRequest<IEnumerable<EmployeeDto>>;
}

using homeMaintenance.Application.DTOs;
using MediatR;

namespace homeMaintenance.Application.Queries.GetPositions
{
    public record EmployeesQuery(string[]? cities, int? price,int? experience, bool? excludeByContract, Guid[] categoryIds) : IRequest<IEnumerable<EmployeeDto>>;
}

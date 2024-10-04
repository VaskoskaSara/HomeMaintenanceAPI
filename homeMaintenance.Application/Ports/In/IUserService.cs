using homeMaintenance.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace homeMaintenance.Application.Ports.In
{
    public interface IUserService
    {
        Task<long?> RegistrationAsync(User user, CancellationToken cancellationToken = default);
        Task<IEnumerable<Position>?> GetPositions(CancellationToken cancellationToken = default);
        Task<bool> LoginAsync(User user, CancellationToken cancellationToken = default);
        Task<bool?> UploadImageToS3(IFormFile file); 
        Task<IEnumerable<EmployeeDto>> GetEmployees(string[]? cities, int? price, int? experience, bool? excludeByContract,Guid[] categoryIds, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>?> GetCities(CancellationToken cancellationToken = default);

    }
}

using homeMaintenance.Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;

namespace homeMaintenance.Infrastructure.HostedServices
{
    public class SeedDataHostedService : IHostedService
    {
        private readonly PositionRepository _positionRepository;

        public SeedDataHostedService(PositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _positionRepository.SeedPositionsAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

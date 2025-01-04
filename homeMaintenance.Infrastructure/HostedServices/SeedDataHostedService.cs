using homeMaintenance.Infrastructure.Adapters.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace homeMaintenance.Infrastructure.HostedServices
{
    public class SeedDataHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public SeedDataHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var positionRepository = scope.ServiceProvider.GetRequiredService<PositionRepository>();

                await positionRepository.SeedPositionsAsync();
            }        
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

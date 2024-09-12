using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineVotingS.Application.Services.ElectionStatusServices;

namespace OnlineVotingS.Infrastructure.BackgroundServices;

public class ElectionStatusUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);

    public ElectionStatusUpdateService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var electionStatusService = scope.ServiceProvider.GetRequiredService<IElectionStatusService>();
                await electionStatusService.UpdateElectionStatuses();
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
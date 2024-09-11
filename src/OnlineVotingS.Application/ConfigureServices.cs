using Microsoft.Extensions.DependencyInjection;
using OnlineVotingS.Application.Services.ElectionStatusServices;
using System.Reflection;

namespace OnlineVotingS.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IElectionStatusService, ElectionStatusService>();

        return services;
    }
}
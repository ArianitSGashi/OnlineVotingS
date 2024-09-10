using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineVotingS.Domain.Interfaces;
using OnlineVotingS.Infrastructure.Persistence.Context;
using OnlineVotingS.Domain.Models;
using OnlineVotingS.Infrastructure.Repositories;
using OnlineVotingS.Infrastructure.BackgroundServices;
using OnlineVotingS.Application.Services.ElectionStatusServices;

namespace OnlineVotingS.Infrastructure;

public static class Startup
{
    public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("OnlineVotingS.Infrastructure")));

        // Setting Up Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
         .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();

        // Register the generic repository
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Register repository interfaces and their implementations
        services.AddScoped<ICandidateRepository, CandidateRepository>();
        services.AddScoped<IElectionRepository, ElectionRepository>();
        services.AddScoped<IComplaintRepository, ComplaintRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<IResultRepository, ResultRepository>();
        services.AddScoped<IVotesRepository, VotesRepository>();
        services.AddScoped<ICampaignRepository, CampaignRepository>();
        services.AddScoped<IRepliedComplaintsRepository, RepliedComplaintsRepository>();
        services.AddScoped<IElectionStatusService, ElectionStatusService>();
        services.AddHostedService<ElectionStatusUpdateService>();
    }
}
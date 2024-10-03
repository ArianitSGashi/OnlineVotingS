using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Configurations;
using OnlineVotingS.Domain.Entities;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.Infrastructure.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Elections> Elections { get; set; }
    public DbSet<Candidates> Candidates { get; set; }
    public DbSet<Votes> Votes { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<Complaints> Complaints { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<RepliedComplaints> RepliedComplaints { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations
        modelBuilder.ApplyConfiguration(new ElectionsConfiguration());
        modelBuilder.ApplyConfiguration(new CandidatesConfiguration());
        modelBuilder.ApplyConfiguration(new VotesConfiguration());
        modelBuilder.ApplyConfiguration(new ResultConfiguration());
        //modelBuilder.ApplyConfiguration(new ComplaintsConfiguration());
        //modelBuilder.ApplyConfiguration(new CampaignConfiguration());
        //modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
        //modelBuilder.ApplyConfiguration(new RepliedComplaintsConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

        // Seed roles
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "5be9341f-b95e-4155-a0f2-ca8f188a1d7b",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "7d5cf4ed-83e2-4c71-a8e8-03c2e6a572aa"
            },
            new IdentityRole
            {
                Id = "6cd1fa1e-c4c6-4a16-a8b2-3f4c031ec0d3",
                Name = "Voter",
                NormalizedName = "VOTER",
                ConcurrencyStamp = "9d5bf4ce-7a3e-9c15-b2e9-04d3e7a683bb"
            }
        );
    }
}
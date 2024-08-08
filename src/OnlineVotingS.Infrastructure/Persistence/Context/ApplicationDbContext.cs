using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace OnlineVotingS.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Elections> Elections { get; set; }
        public DbSet<Candidates> Candidates { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Complaints> Complaints { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            // Election entity
            modelBuilder.Entity<Elections>(entity =>
            {
                entity.HasKey(e => e.ElectionID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasMany(e => e.Candidates)
                      .WithOne(c => c.Elections)
                      .HasForeignKey(c => c.ElectionID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Votes)
                      .WithOne(v => v.Elections)
                      .HasForeignKey(v => v.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Results)
                      .WithOne(r => r.Elections)
                      .HasForeignKey(r => r.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Complaints)
                      .WithOne(c => c.Elections)
                      .HasForeignKey(c => c.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Campaigns)
                      .WithOne(c => c.Elections)
                      .HasForeignKey(c => c.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Feedbacks)
                      .WithOne(f => f.Elections)
                      .HasForeignKey(f => f.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Candidate entity
            modelBuilder.Entity<Candidates>(entity =>
            {
                entity.HasKey(c => c.CandidateID);
                entity.Property(c => c.FullName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Party).HasMaxLength(50);
                entity.Property(c => c.Description).HasMaxLength(100);
                entity.Property(c => c.Income).HasColumnType("decimal(10,2)");
                entity.Property(c => c.Works).HasMaxLength(100);

                entity.HasOne(c => c.Elections)
                      .WithMany(e => e.Candidates)
                      .HasForeignKey(c => c.ElectionID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Votes)
                      .WithOne(v => v.Candidates)
                      .HasForeignKey(v => v.CandidateID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Results)
                      .WithOne(r => r.Candidates)
                      .HasForeignKey(r => r.CandidateID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Campaigns)
                      .WithOne(c => c.Candidates)
                      .HasForeignKey(c => c.CandidateID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Vote entity
            modelBuilder.Entity<Votes>(entity =>
            {
                entity.HasKey(v => v.VoteID);
                entity.Property(v => v.UserID).IsRequired();
                entity.Property(v => v.VoteDate).IsRequired();

                entity.HasOne(v => v.Elections)
                      .WithMany(e => e.Votes)
                      .HasForeignKey(v => v.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(v => v.Candidates)
                      .WithMany(c => c.Votes)
                      .HasForeignKey(v => v.CandidateID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(v => v.User)
                      .WithMany()
                      .HasForeignKey(v => v.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Result entity
            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasKey(r => r.ResultID);
                entity.Property(r => r.TotalVotes).IsRequired();

                entity.HasOne(r => r.Elections)
                      .WithMany(e => e.Results)
                      .HasForeignKey(r => r.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Candidates)
                      .WithMany(c => c.Results)
                      .HasForeignKey(r => r.CandidateID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Complaint entity
            modelBuilder.Entity<Complaints>(entity =>
            {
                entity.HasKey(c => c.ComplaintID);
                entity.Property(c => c.UserID).IsRequired();
                entity.Property(c => c.ComplaintText).IsRequired().HasMaxLength(200);
                entity.Property(c => c.ComplaintDate).IsRequired();

                entity.HasOne(c => c.Elections)
                      .WithMany(e => e.Complaints)
                      .HasForeignKey(c => c.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.User)
                      .WithMany()
                      .HasForeignKey(c => c.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Campaign entity
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasKey(c => c.CampaignID);
                entity.Property(c => c.Description).HasMaxLength(100);
                entity.Property(c => c.StartDate).IsRequired();
                entity.Property(c => c.EndDate).IsRequired();
                entity.Property(c => c.CreatedAt).IsRequired();
                entity.Property(c => c.UpdatedAt).IsRequired();

                entity.HasOne(c => c.Elections)
                      .WithMany(e => e.Campaigns)
                      .HasForeignKey(c => c.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Candidates)
                      .WithMany(c => c.Campaigns)
                      .HasForeignKey(c => c.CandidateID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Feedback entity
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(f => f.FeedbackID);
                entity.Property(f => f.UserID).IsRequired();
                entity.Property(f => f.FeedbackText).IsRequired().HasMaxLength(200);
                entity.Property(f => f.FeedbackDate).IsRequired();

                entity.HasOne(f => f.Elections)
                      .WithMany(e => e.Feedbacks)
                      .HasForeignKey(f => f.ElectionID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.User)
                      .WithMany()
                      .HasForeignKey(f => f.UserID)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

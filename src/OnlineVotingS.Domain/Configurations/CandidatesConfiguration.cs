using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Domain.Configurations;

public class CandidatesConfiguration : IEntityTypeConfiguration<Candidates>
{
    public void Configure(EntityTypeBuilder<Candidates> builder)
    {
        builder.HasKey(c => c.CandidateID);
        builder.Property(c => c.ElectionID).IsRequired();
        builder.Property(c => c.FullName).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Party).HasMaxLength(50);
        builder.Property(c => c.Description).HasMaxLength(100);
        builder.Property(c => c.Income).HasColumnType("decimal(10,2)");
        builder.Property(c => c.Works).HasMaxLength(100);

        builder.HasOne(c => c.Elections)
               .WithMany(e => e.Candidates)
               .HasForeignKey(c => c.ElectionID)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Votes)
               .WithOne(v => v.Candidates)
               .HasForeignKey(v => v.CandidateID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Results)
               .WithOne(r => r.Candidates)
               .HasForeignKey(r => r.CandidateID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Campaigns)
               .WithOne(c => c.Candidates)
               .HasForeignKey(c => c.CandidateID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => new { c.ElectionID, c.Party }).IsUnique();
    }
}
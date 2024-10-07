using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Domain.Configurations;

public class ElectionsConfiguration : IEntityTypeConfiguration<Elections>
{
    public void Configure(EntityTypeBuilder<Elections> builder)
    {
        builder.HasKey(e => e.ElectionID);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Description).HasMaxLength(100);

        builder.HasMany(e => e.Candidates)
               .WithOne(c => c.Elections)
               .HasForeignKey(c => c.ElectionID)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Votes)
               .WithOne(v => v.Elections)
               .HasForeignKey(v => v.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Results)
               .WithOne(r => r.Elections)
               .HasForeignKey(r => r.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Complaints)
               .WithOne(c => c.Elections)
               .HasForeignKey(c => c.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Campaigns)
               .WithOne(c => c.Elections)
               .HasForeignKey(c => c.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
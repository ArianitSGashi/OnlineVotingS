using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Domain.Configurations;

public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.HasKey(r => r.ResultID);
        builder.Property(r => r.TotalVotes).IsRequired();

        builder.HasOne(r => r.Elections)
               .WithMany(e => e.Results)
               .HasForeignKey(r => r.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Candidates)
               .WithMany(c => c.Results)
               .HasForeignKey(r => r.CandidateID)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
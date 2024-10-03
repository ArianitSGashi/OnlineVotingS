using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Entities;

namespace OnlineVotingS.Domain.Configurations;

public class VotesConfiguration : IEntityTypeConfiguration<Votes>
{
    public void Configure(EntityTypeBuilder<Votes> builder)
    {
        builder.HasKey(v => v.VoteID);
        builder.Property(v => v.UserID).IsRequired();
        builder.Property(v => v.VoteDate).IsRequired();
        builder.Property(v => v.ElectionID).IsRequired();
        builder.Property(v => v.CandidateID).IsRequired();

        builder.HasOne(v => v.Elections)
               .WithMany(e => e.Votes)
               .HasForeignKey(v => v.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Candidates)
               .WithMany(c => c.Votes)
               .HasForeignKey(v => v.CandidateID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.User)
               .WithMany()
               .HasForeignKey(v => v.UserID)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => new { v.UserID, v.ElectionID }).IsUnique();
    }
}
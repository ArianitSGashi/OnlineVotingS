using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.HasKey(c => c.CampaignID);
        builder.Property(c => c.Description).HasMaxLength(100);
        builder.Property(c => c.StartDate).IsRequired();
        builder.Property(c => c.EndDate).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();

        builder.HasOne(c => c.Elections)
              .WithMany(e => e.Campaigns)
              .HasForeignKey(c => c.ElectionID)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Candidates)
              .WithMany(c => c.Campaigns)
              .HasForeignKey(c => c.CandidateID)
              .OnDelete(DeleteBehavior.Restrict);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Configurations;

public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasKey(f => f.FeedbackID);
        builder.Property(f => f.UserID).IsRequired();
        builder.Property(f => f.FeedbackText).IsRequired().HasMaxLength(200);
        builder.Property(f => f.FeedbackDate).IsRequired();

        builder.HasOne(f => f.Elections)
              .WithMany(e => e.Feedbacks)
              .HasForeignKey(f => f.ElectionID)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.User)
              .WithMany()
              .HasForeignKey(f => f.UserID)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

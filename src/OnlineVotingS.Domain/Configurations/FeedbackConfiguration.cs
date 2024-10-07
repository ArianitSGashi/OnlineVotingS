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
        builder.Property(f => f.FeedbackText).IsRequired().HasMaxLength(200);
        builder.Property(f => f.FeedbackDate).IsRequired();
        builder.Property(f => f.FeedbackCategory).IsRequired();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Configurations;

public class RepliedComplaintsConfiguration : IEntityTypeConfiguration<RepliedComplaints>
{
    public void Configure(EntityTypeBuilder<RepliedComplaints> builder)
    {
        builder.HasKey(rc => rc.RepliedComplaintID);
        builder.Property(rc => rc.ComplaintID).IsRequired();
        builder.Property(rc => rc.ReplyText).IsRequired().HasMaxLength(200);
        builder.Property(rc => rc.ReplyDate).IsRequired();

        builder.HasOne(rc => rc.Complaint)
          .WithMany(c => c.RepliedComplaints)
          .HasForeignKey(rc => rc.ComplaintID)
          .OnDelete(DeleteBehavior.Cascade);
    }
}

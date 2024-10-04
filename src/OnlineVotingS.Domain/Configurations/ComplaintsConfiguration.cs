using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineVotingS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVotingS.Domain.Configurations;

public class ComplaintsConfiguration : IEntityTypeConfiguration<Complaints>
{
    public void Configure(EntityTypeBuilder<Complaints> builder)
    {
        builder.HasKey(c => c.ComplaintID);
        builder.Property(c => c.UserID).IsRequired();
        builder.Property(c => c.ComplaintText).IsRequired().HasMaxLength(200);
        builder.Property(c => c.ComplaintDate).IsRequired();

        builder.HasOne(c => c.Elections)
               .WithMany(e => e.Complaints)
               .HasForeignKey(c => c.ElectionID)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserID)
               .OnDelete(DeleteBehavior.Cascade); 
    }
}

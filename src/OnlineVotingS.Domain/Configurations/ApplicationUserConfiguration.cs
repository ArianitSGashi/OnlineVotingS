using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineVotingS.Domain.Models;

namespace OnlineVotingS.Domain.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("AspNetUsers"); // Maps to the existing AspNetUsers table
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.FathersName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Gender).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.Address).IsRequired().HasMaxLength(200);
    }
}
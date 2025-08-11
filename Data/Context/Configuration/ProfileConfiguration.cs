using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context.Configuration;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.HasKey(p => p.ProfileId);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.HasMany(p => p.Users)
            .WithOne(u => u.Profile) // Asegúrate que User tenga propiedad Profile
            .HasForeignKey(u => u.ProfileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
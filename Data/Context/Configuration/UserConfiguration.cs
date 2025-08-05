using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Correo)
                   .IsRequired()
                   .HasMaxLength(150);

            // Relación uno a muchos con Task (si Task tiene una propiedad UserId y navegación User)
            builder.HasMany(u => u.Tasks)
                   .WithOne()
                   .HasForeignKey("UserId") // Clave foránea en Task
                   .OnDelete(DeleteBehavior.Cascade); // O Restrict, según tus reglas
        }
    }
}

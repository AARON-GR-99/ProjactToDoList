    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Data.Entities; // Asegúrate de que el namespace coincida con donde está tu clase Category

    namespace Data.Context.Configuration
    {
        public class CategoryConfiguration : IEntityTypeConfiguration<Category>
        {
            public void Configure(EntityTypeBuilder<Category> builder)
            {
                builder.ToTable("Categories"); // Nombre de la tabla

                builder.HasKey(c => c.CategoryId); // Clave primaria

                builder.Property(c => c.CategoryName)
                       .IsRequired()
                       .HasMaxLength(100);

                // Relación uno a muchos con Task (si Task tiene CategoryId y navegación Category)
                builder.HasMany(c => c.tasks)
                       .WithOne()
                       .HasForeignKey("CategoryId")
                       .OnDelete(DeleteBehavior.Cascade); // Puede ser Restrict si prefieres
            }
        }
    }


    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Data.Entities;

    namespace Data.Context.Configuration;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);

            builder.HasMany(c => c.tasks)
                .WithOne()
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
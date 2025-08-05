using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Entities.Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.TaskId);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000);

            builder.Property(t => t.IsCompleted)
                   .IsRequired();

            builder.Property(t => t.CreatedDate)
                   .IsRequired();

            // Relación con User (uno a muchos)
            builder.HasOne(t => t.User)
                   .WithMany(u => u.Tasks)
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Puedes cambiar a Restrict si lo prefieres

            // Relación con Category (uno a muchos)
            builder.HasOne(t => t.Category)
                   .WithMany(c => c.tasks)
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Para evitar borrado en cascada si lo deseas
        }
    }
}

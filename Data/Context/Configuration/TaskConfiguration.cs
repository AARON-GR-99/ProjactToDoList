using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Context.Configuration;

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

              builder.HasOne(t => t.User)
                     .WithMany(u => u.Tasks)
                     .HasForeignKey(t => t.UserId)
                     .OnDelete(DeleteBehavior.Restrict); 

              builder.HasOne(t => t.Category)
                     .WithMany(c => c.tasks)
                     .HasForeignKey(t => t.CategoryId)
                     .OnDelete(DeleteBehavior.Restrict); 
       }
}
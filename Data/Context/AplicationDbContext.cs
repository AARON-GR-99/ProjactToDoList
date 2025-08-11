using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<Entities.Task> Task { get; set; } = null!;
    public DbSet<Profile> Profile { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
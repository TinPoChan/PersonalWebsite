using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext (options)
{
    public DbSet<Project> Projects => Set<Project>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<ProjectTag> ProjectTags => Set<ProjectTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(
            new { TagId = 1, Name = "C#" },
            new { TagId = 2, Name = "ASP.NET Core" },
            new { TagId = 3, Name = "Entity Framework Core" },
            new { TagId = 4, Name = "Blazor" },
            new { TagId = 5, Name = "Azure" }
        );

        // Configure the many-to-many relationship
        modelBuilder.Entity<ProjectTag>()
            .HasKey(pt => new { pt.ProjectId, pt.TagId });

        modelBuilder.Entity<ProjectTag>()
            .HasOne(pt => pt.Project)
            .WithMany(p => p.ProjectTags)
            .HasForeignKey(pt => pt.ProjectId);

        modelBuilder.Entity<ProjectTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProjectTags)
            .HasForeignKey(pt => pt.TagId);
    }
}

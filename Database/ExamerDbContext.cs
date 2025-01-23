using Microsoft.EntityFrameworkCore;
using Examer.Models;

namespace Examer.Database;

public class ExamerDbContext : DbContext
{
    public ExamerDbContext(DbContextOptions<ExamerDbContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Exam>? Exams { get; set; }
    public DbSet<Problem>? Problems { get; set; }
    public DbSet<ExamUser>? ExamUsers { get; set; }
    public DbSet<Commit>? Commits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Exams)
            .WithMany(e => e.Users)
            .UsingEntity<ExamUser>();

        modelBuilder.Entity<User>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Exam>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Problem>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ExamUser>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Commit>()
            .HasQueryFilter(e => !e.IsDeleted);
    }
}

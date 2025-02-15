using Microsoft.EntityFrameworkCore;
using Examer.Models;

namespace Examer.Database;

public class ExamerDbContext : DbContext
{
    public ExamerDbContext(DbContextOptions<ExamerDbContext> options) : base(options)
    {

    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Group>? Groups { get; set; }
    public DbSet<Exam>? Exams { get; set; }
    public DbSet<ExamInheritance>? ExamInheritances { get; set; }
    public DbSet<Problem>? Problems { get; set; }
    public DbSet<UserExam>? UserExams { get; set; }
    public DbSet<Commit>? Commits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Exams)
            .WithMany(e => e.Users)
            .UsingEntity<UserExam>(
                l => l.HasOne(e => e.Exam).WithMany(e => e.UserExams).HasForeignKey(e => e.ExamId),
                r => r.HasOne(e => e.User).WithMany(e => e.UserExams).HasForeignKey(e => e.UserId)
            );
        
        modelBuilder.Entity<User>()
            .HasMany(e => e.GroupUsers)
            .WithMany(e => e.UsersOfGroup)
            .UsingEntity<Group>(
                l => l.HasOne(e => e.GroupUser).WithMany(e => e.Groups).HasForeignKey(e => e.GroupId),
                r => r.HasOne(e => e.User).WithMany(e => e.Users).HasForeignKey(e => e.UserOfGroupId)
            );
        
        modelBuilder.Entity<UserExam>(
            nestedBuilder =>
            {
                nestedBuilder.HasKey(e => new { e.UserId, e.ExamId });

                nestedBuilder.HasMany(e => e.Commits)
                    .WithOne(e => e.UserExam)
                    .HasPrincipalKey(e => new { e.UserId, e.ExamId })
                    .HasForeignKey(e => new { e.UserId, e.ExamId })
                    .IsRequired();
            }
        );

        modelBuilder.Entity<Exam>()
            .HasMany(e => e.InheritedExam)
            .WithMany(e => e.InheritingExam)
            .UsingEntity<ExamInheritance>(
                l => l.HasOne(e => e.InheritedExam).WithMany(e => e.InheritedExamInheritance).HasForeignKey(e => e.InheritedExamId),
                r => r.HasOne(e => e.InheritingExam).WithMany(e => e.InheritingExamInheritance).HasForeignKey(e => e.InheritingExamId)
            );

        modelBuilder.Entity<User>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Group>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Exam>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<ExamInheritance>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Problem>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<UserExam>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Commit>()
            .HasQueryFilter(e => !e.IsDeleted);
    }
}

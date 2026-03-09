// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Database;

public class ExamerDbContext(DbContextOptions<ExamerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<UserExam> UserExams { get; set; }
    public DbSet<Commit> Commits { get; set; }
    public DbSet<Marking> Markings { get; set; }
    public DbSet<ExamerFile> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<User>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<UserDetail>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Group>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Exam>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Problem>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<UserExam>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Commit>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Marking>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<ExamerFile>()
            .HasQueryFilter(e => e.DeletedAt == null)
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<User>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<UserDetail>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Group>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Exam>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Problem>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<UserExam>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Commit>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<Marking>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<ExamerFile>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        modelBuilder.Entity<User>()
            .Property(e => e.Enabled)
            .HasDefaultValue(false);

        modelBuilder.Entity<Exam>()
            .Property(e => e.IsPublic)
            .HasDefaultValue(false);

        modelBuilder.Entity<User>()
            .Property(e => e.EmailActivateToken)
            .HasDefaultValueSql("uuid_generate_v4()");
    }
}

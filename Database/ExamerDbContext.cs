// Copyright (c) ZJUKongroo. All Rights Reserved.

using Examer.Models;

using Microsoft.EntityFrameworkCore;

namespace Examer.Database;

public class ExamerDbContext(DbContextOptions<ExamerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<UserExam> UserExams { get; set; }
    public DbSet<Commit> Commits { get; set; }
    public DbSet<Marking> Markings { get; set; }
    public DbSet<ExamerFile> Files { get; set; }

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

        modelBuilder.Entity<User>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<Group>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<Exam>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<Problem>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<UserExam>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<Commit>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<Marking>()
            .HasQueryFilter(e => e.DeletedAt == null);
        modelBuilder.Entity<ExamerFile>()
            .HasQueryFilter(e => e.DeletedAt == null);
    }
}

﻿// <auto-generated />
using System;
using Examer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examer.Migrations
{
    [DbContext(typeof(ExamerDbContext))]
    [Migration("20250222075831_Create-Files-Table")]
    partial class CreateFilesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Examer.Models.Commit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CommitTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserExamId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.HasIndex("UserExamId");

                    b.ToTable("Commits");
                });

            modelBuilder.Entity("Examer.Models.Exam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExamType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Examer.Models.ExamInheritance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("InheritedExamId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("InheritingExamId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InheritedExamId");

                    b.HasIndex("InheritingExamId");

                    b.ToTable("ExamInheritances");
                });

            modelBuilder.Entity("Examer.Models.ExamerFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CommitId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<int>("FileType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ProblemId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommitId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Examer.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserOfGroupId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserOfGroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Examer.Models.Marking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CommitId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ReviewUserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommitId");

                    b.HasIndex("ReviewUserId");

                    b.ToTable("Markings");
                });

            modelBuilder.Entity("Examer.Models.Problem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProblemType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("Examer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Campus")
                        .HasColumnType("TEXT");

                    b.Property<string>("Class")
                        .HasColumnType("TEXT");

                    b.Property<string>("College")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Dormitory")
                        .HasColumnType("TEXT");

                    b.Property<int>("EthnicGroup")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Major")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("TEXT");

                    b.Property<int>("PoliticalStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentNo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Examer.Models.UserExam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ExamId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExams");
                });

            modelBuilder.Entity("Examer.Models.Commit", b =>
                {
                    b.HasOne("Examer.Models.Problem", "Problem")
                        .WithMany("Commits")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examer.Models.UserExam", "UserExam")
                        .WithMany("Commits")
                        .HasForeignKey("UserExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("UserExam");
                });

            modelBuilder.Entity("Examer.Models.ExamInheritance", b =>
                {
                    b.HasOne("Examer.Models.Exam", "InheritedExam")
                        .WithMany("InheritedExamInheritance")
                        .HasForeignKey("InheritedExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examer.Models.Exam", "InheritingExam")
                        .WithMany("InheritingExamInheritance")
                        .HasForeignKey("InheritingExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InheritedExam");

                    b.Navigation("InheritingExam");
                });

            modelBuilder.Entity("Examer.Models.ExamerFile", b =>
                {
                    b.HasOne("Examer.Models.Commit", "Commit")
                        .WithMany("Files")
                        .HasForeignKey("CommitId");

                    b.HasOne("Examer.Models.Problem", "Problem")
                        .WithMany("Files")
                        .HasForeignKey("ProblemId");

                    b.Navigation("Commit");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Examer.Models.Group", b =>
                {
                    b.HasOne("Examer.Models.User", "GroupUser")
                        .WithMany("Groups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examer.Models.User", "User")
                        .WithMany("Users")
                        .HasForeignKey("UserOfGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GroupUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Examer.Models.Marking", b =>
                {
                    b.HasOne("Examer.Models.Commit", "Commit")
                        .WithMany("Markings")
                        .HasForeignKey("CommitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examer.Models.User", "ReviewUser")
                        .WithMany("Markings")
                        .HasForeignKey("ReviewUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commit");

                    b.Navigation("ReviewUser");
                });

            modelBuilder.Entity("Examer.Models.Problem", b =>
                {
                    b.HasOne("Examer.Models.Exam", "Exam")
                        .WithMany("Problems")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("Examer.Models.UserExam", b =>
                {
                    b.HasOne("Examer.Models.Exam", "Exam")
                        .WithMany("UserExams")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examer.Models.User", "User")
                        .WithMany("UserExams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Examer.Models.Commit", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Markings");
                });

            modelBuilder.Entity("Examer.Models.Exam", b =>
                {
                    b.Navigation("InheritedExamInheritance");

                    b.Navigation("InheritingExamInheritance");

                    b.Navigation("Problems");

                    b.Navigation("UserExams");
                });

            modelBuilder.Entity("Examer.Models.Problem", b =>
                {
                    b.Navigation("Commits");

                    b.Navigation("Files");
                });

            modelBuilder.Entity("Examer.Models.User", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Markings");

                    b.Navigation("UserExams");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Examer.Models.UserExam", b =>
                {
                    b.Navigation("Commits");
                });
#pragma warning restore 612, 618
        }
    }
}

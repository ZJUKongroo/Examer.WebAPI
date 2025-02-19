using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExams",
                table: "UserExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamInheritances",
                table: "ExamInheritances");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserExams",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Groups",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ExamInheritances",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserExams_UserId_ExamId",
                table: "UserExams",
                columns: new[] { "UserId", "ExamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExams",
                table: "UserExams",
                columns: new[] { "Id", "UserId", "ExamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                columns: new[] { "Id", "GroupId", "UserOfGroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamInheritances",
                table: "ExamInheritances",
                columns: new[] { "Id", "InheritedExamId", "InheritingExamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupId",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamInheritances_InheritedExamId",
                table: "ExamInheritances",
                column: "InheritedExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserExams_UserId_ExamId",
                table: "UserExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExams",
                table: "UserExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamInheritances",
                table: "ExamInheritances");

            migrationBuilder.DropIndex(
                name: "IX_ExamInheritances_InheritedExamId",
                table: "ExamInheritances");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserExams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExamInheritances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExams",
                table: "UserExams",
                columns: new[] { "UserId", "ExamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamInheritances",
                table: "ExamInheritances",
                columns: new[] { "InheritedExamId", "InheritingExamId" });
        }
    }
}

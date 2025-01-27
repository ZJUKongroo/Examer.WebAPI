using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class ExamInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Commits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExamInheritances",
                columns: table => new
                {
                    InheritedExamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InheritingExamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamInheritances", x => new { x.InheritedExamId, x.InheritingExamId });
                    table.ForeignKey(
                        name: "FK_ExamInheritances_Exams_InheritedExamId",
                        column: x => x.InheritedExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamInheritances_Exams_InheritingExamId",
                        column: x => x.InheritingExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamInheritances_InheritingExamId",
                table: "ExamInheritances",
                column: "InheritingExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamInheritances");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Commits");
        }
    }
}

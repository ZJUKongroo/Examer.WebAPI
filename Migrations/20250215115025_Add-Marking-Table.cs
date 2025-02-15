using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class AddMarkingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Commits");

            migrationBuilder.AddColumn<int>(
                name: "ExamType",
                table: "Exams",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Commits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Markings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CommitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReviewUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markings_Commits_CommitId",
                        column: x => x.CommitId,
                        principalTable: "Commits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Markings_Users_ReviewUserId",
                        column: x => x.ReviewUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Markings_CommitId",
                table: "Markings",
                column: "CommitId");

            migrationBuilder.CreateIndex(
                name: "IX_Markings_ReviewUserId",
                table: "Markings",
                column: "ReviewUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Markings");

            migrationBuilder.DropColumn(
                name: "ExamType",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Commits");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Commits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

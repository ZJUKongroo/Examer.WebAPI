using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class CreateFilesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Commits");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    ProblemId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CommitId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FileType = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Commits_CommitId",
                        column: x => x.CommitId,
                        principalTable: "Commits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Files_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CommitId",
                table: "Files",
                column: "CommitId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ProblemId",
                table: "Files",
                column: "ProblemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Problems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Commits",
                type: "TEXT",
                nullable: true);
        }
    }
}

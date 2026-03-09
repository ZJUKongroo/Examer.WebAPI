using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class MakeFileParentNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_file_commit_commit_id",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_file_problem_problem_id",
                table: "file");

            migrationBuilder.AlterColumn<Guid>(
                name: "problem_id",
                table: "file",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "commit_id",
                table: "file",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_file_commit_commit_id",
                table: "file",
                column: "commit_id",
                principalTable: "commit",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_file_problem_problem_id",
                table: "file",
                column: "problem_id",
                principalTable: "problem",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_file_commit_commit_id",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_file_problem_problem_id",
                table: "file");

            migrationBuilder.AlterColumn<Guid>(
                name: "problem_id",
                table: "file",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "commit_id",
                table: "file",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_file_commit_commit_id",
                table: "file",
                column: "commit_id",
                principalTable: "commit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_file_problem_problem_id",
                table: "file",
                column: "problem_id",
                principalTable: "problem",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

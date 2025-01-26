using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProblemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_ExamUsers_UserId_ExamId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamUsers_Exams_ExamId",
                table: "ExamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamUsers_Users_UserId",
                table: "ExamUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamUsers",
                table: "ExamUsers");

            migrationBuilder.RenameTable(
                name: "ExamUsers",
                newName: "UserExams");

            migrationBuilder.RenameIndex(
                name: "IX_ExamUsers_ExamId",
                table: "UserExams",
                newName: "IX_UserExams_ExamId");

            migrationBuilder.AddColumn<string>(
                name: "StorageLocation",
                table: "Problems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserExams",
                table: "UserExams",
                columns: new[] { "UserId", "ExamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_UserExams_UserId_ExamId",
                table: "Commits",
                columns: new[] { "UserId", "ExamId" },
                principalTable: "UserExams",
                principalColumns: new[] { "UserId", "ExamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExams_Exams_ExamId",
                table: "UserExams",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExams_Users_UserId",
                table: "UserExams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_UserExams_UserId_ExamId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExams_Exams_ExamId",
                table: "UserExams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExams_Users_UserId",
                table: "UserExams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserExams",
                table: "UserExams");

            migrationBuilder.DropColumn(
                name: "StorageLocation",
                table: "Problems");

            migrationBuilder.RenameTable(
                name: "UserExams",
                newName: "ExamUsers");

            migrationBuilder.RenameIndex(
                name: "IX_UserExams_ExamId",
                table: "ExamUsers",
                newName: "IX_ExamUsers_ExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                columns: new[] { "GroupId", "UserOfGroupId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamUsers",
                table: "ExamUsers",
                columns: new[] { "UserId", "ExamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_ExamUsers_UserId_ExamId",
                table: "Commits",
                columns: new[] { "UserId", "ExamId" },
                principalTable: "ExamUsers",
                principalColumns: new[] { "UserId", "ExamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamUsers_Exams_ExamId",
                table: "ExamUsers",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamUsers_Users_UserId",
                table: "ExamUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

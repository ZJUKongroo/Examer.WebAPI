using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_ExamUsers_ExamUserExamsId_ExamUserUsersId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamUsers_Exams_ExamsId",
                table: "ExamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamUsers_Users_UsersId",
                table: "ExamUsers");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Commits_ExamUserExamsId_ExamUserUsersId",
                table: "Commits");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "ExamUsers",
                newName: "ExamId");

            migrationBuilder.RenameColumn(
                name: "ExamsId",
                table: "ExamUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamUsers_UsersId",
                table: "ExamUsers",
                newName: "IX_ExamUsers_ExamId");

            migrationBuilder.RenameColumn(
                name: "ExamUserUsersId",
                table: "Commits",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ExamUserId",
                table: "Commits",
                newName: "ProblemId");

            migrationBuilder.RenameColumn(
                name: "ExamUserExamsId",
                table: "Commits",
                newName: "ExamId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserOfGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => new { x.GroupId, x.UserOfGroupId });
                    table.ForeignKey(
                        name: "FK_Groups_Users_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Users_UserOfGroupId",
                        column: x => x.UserOfGroupId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commits_ProblemId",
                table: "Commits",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_UserId_ExamId",
                table: "Commits",
                columns: new[] { "UserId", "ExamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserOfGroupId",
                table: "Groups",
                column: "UserOfGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_ExamUsers_UserId_ExamId",
                table: "Commits",
                columns: new[] { "UserId", "ExamId" },
                principalTable: "ExamUsers",
                principalColumns: new[] { "UserId", "ExamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Problems_ProblemId",
                table: "Commits",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_ExamUsers_UserId_ExamId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Problems_ProblemId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamUsers_Exams_ExamId",
                table: "ExamUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamUsers_Users_UserId",
                table: "ExamUsers");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Commits_ProblemId",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_UserId_ExamId",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "ExamUsers",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ExamUsers",
                newName: "ExamsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamUsers_ExamId",
                table: "ExamUsers",
                newName: "IX_ExamUsers_UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Commits",
                newName: "ExamUserUsersId");

            migrationBuilder.RenameColumn(
                name: "ProblemId",
                table: "Commits",
                newName: "ExamUserId");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "Commits",
                newName: "ExamUserExamsId");

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersOfGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => new { x.GroupsId, x.UsersOfGroupId });
                    table.ForeignKey(
                        name: "FK_Group_Users_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Group_Users_UsersOfGroupId",
                        column: x => x.UsersOfGroupId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commits_ExamUserExamsId_ExamUserUsersId",
                table: "Commits",
                columns: new[] { "ExamUserExamsId", "ExamUserUsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_Group_UsersOfGroupId",
                table: "Group",
                column: "UsersOfGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_ExamUsers_ExamUserExamsId_ExamUserUsersId",
                table: "Commits",
                columns: new[] { "ExamUserExamsId", "ExamUserUsersId" },
                principalTable: "ExamUsers",
                principalColumns: new[] { "ExamsId", "UsersId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamUsers_Exams_ExamsId",
                table: "ExamUsers",
                column: "ExamsId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamUsers_Users_UsersId",
                table: "ExamUsers",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

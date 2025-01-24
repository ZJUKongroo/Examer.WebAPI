using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommitTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Problems_ProblemId",
                table: "Commits");

            migrationBuilder.DropForeignKey(
                name: "FK_Commits_Users_UserId",
                table: "Commits");

            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.DropIndex(
                name: "IX_Commits_ProblemId",
                table: "Commits");

            migrationBuilder.DropIndex(
                name: "IX_Commits_UserId",
                table: "Commits");

            migrationBuilder.RenameColumn(
                name: "ModifyTime",
                table: "Users",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "ModifyTime",
                table: "Problems",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "ModifyTime",
                table: "ExamUsers",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "ModifyTime",
                table: "Exams",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Commits",
                newName: "UpdateTime");

            migrationBuilder.RenameColumn(
                name: "ProblemId",
                table: "Commits",
                newName: "ExamUserUsersId");

            migrationBuilder.RenameColumn(
                name: "ModifyTime",
                table: "Commits",
                newName: "ExamUserId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Problems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExamUserExamsId",
                table: "Commits",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersOfGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commits_ExamUsers_ExamUserExamsId_ExamUserUsersId",
                table: "Commits");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Commits_ExamUserExamsId_ExamUserUsersId",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "ExamUserExamsId",
                table: "Commits");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Users",
                newName: "ModifyTime");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Problems",
                newName: "ModifyTime");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "ExamUsers",
                newName: "ModifyTime");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Exams",
                newName: "ModifyTime");

            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Commits",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ExamUserUsersId",
                table: "Commits",
                newName: "ProblemId");

            migrationBuilder.RenameColumn(
                name: "ExamUserId",
                table: "Commits",
                newName: "ModifyTime");

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    GroupsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersOfGroupId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.GroupsId, x.UsersOfGroupId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_UsersOfGroupId",
                        column: x => x.UsersOfGroupId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commits_ProblemId",
                table: "Commits",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Commits_UserId",
                table: "Commits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_UsersOfGroupId",
                table: "UserUser",
                column: "UsersOfGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Problems_ProblemId",
                table: "Commits",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commits_Users_UserId",
                table: "Commits",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

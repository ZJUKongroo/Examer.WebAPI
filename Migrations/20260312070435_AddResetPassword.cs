using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "email_activate_token",
                table: "user",
                newName: "activate_account_token");

            migrationBuilder.RenameIndex(
                name: "user_email_activate_token_uidx",
                table: "user",
                newName: "user_activate_account_token_uidx");

            migrationBuilder.AddColumn<Guid>(
                name: "reset_password_token",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "user_reset_password_token_uidx",
                table: "user",
                column: "reset_password_token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "user_reset_password_token_uidx",
                table: "user");

            migrationBuilder.DropColumn(
                name: "reset_password_token",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "activate_account_token",
                table: "user",
                newName: "email_activate_token");

            migrationBuilder.RenameIndex(
                name: "user_activate_account_token_uidx",
                table: "user",
                newName: "user_email_activate_token_uidx");
        }
    }
}

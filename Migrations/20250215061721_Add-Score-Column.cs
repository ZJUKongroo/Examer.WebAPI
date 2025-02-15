using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class AddScoreColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Problems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Commits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Commits");
        }
    }
}

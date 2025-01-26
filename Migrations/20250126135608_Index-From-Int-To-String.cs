using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examer.Migrations
{
    /// <inheritdoc />
    public partial class IndexFromIntToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Problems");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Problems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Problems");

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Problems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}

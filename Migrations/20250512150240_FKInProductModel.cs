using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace enterpriseP2.Migrations
{
    /// <inheritdoc />
    public partial class FKInProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FarmerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FarmerId",
                table: "Products");
        }
    }
}

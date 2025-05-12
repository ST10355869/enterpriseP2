using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace enterpriseP2.Migrations
{
    /// <inheritdoc />
    public partial class ModelsCHangedAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Farmer",
                table: "Farmer");

            migrationBuilder.RenameTable(
                name: "Farmer",
                newName: "Farmers");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Farmers",
                table: "Farmers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Farmers",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Farmers");

            migrationBuilder.RenameTable(
                name: "Farmers",
                newName: "Farmer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Farmer",
                table: "Farmer",
                column: "Id");
        }
    }
}

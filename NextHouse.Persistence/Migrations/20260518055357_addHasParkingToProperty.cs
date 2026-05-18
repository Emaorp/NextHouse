using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextHouse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addHasParkingToProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasParking",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasParking",
                table: "Properties");
        }
    }
}

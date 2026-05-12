using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextHouse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updaterequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationsName",
                table: "PropertyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PropertyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "PropertyRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationsName",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "PropertyRequests");
        }
    }
}

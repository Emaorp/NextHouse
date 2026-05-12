using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextHouse.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updaterequests2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRequests_User_TenantId",
                table: "PropertyRequests");

            migrationBuilder.DropIndex(
                name: "IX_PropertyRequests_TenantId",
                table: "PropertyRequests");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PropertyRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "PropertyRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRequests_TenantId",
                table: "PropertyRequests",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRequests_User_TenantId",
                table: "PropertyRequests",
                column: "TenantId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

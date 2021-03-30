using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class checkoutcounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckinCounter",
                table: "Member",
                newName: "CheckOutCounter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOutCounter",
                table: "Member",
                newName: "CheckinCounter");
        }
    }
}

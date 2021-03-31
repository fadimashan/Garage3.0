using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class memebercheckcounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraHourlyRate",
                table: "TypeOfVehicle");

            migrationBuilder.DropColumn(
                name: "ExtraRate",
                table: "TypeOfVehicle");

            migrationBuilder.AddColumn<int>(
                name: "CheckinCounter",
                table: "Member",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckinCounter",
                table: "Member");

            migrationBuilder.AddColumn<int>(
                name: "ExtraHourlyRate",
                table: "TypeOfVehicle",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExtraRate",
                table: "TypeOfVehicle",
                type: "int",
                nullable: true);
        }
    }
}

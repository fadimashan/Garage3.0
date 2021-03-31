using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class frontend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraHourlyRate",
                table: "TypeOfVehicle");

            migrationBuilder.DropColumn(
                name: "ExtraRate",
                table: "TypeOfVehicle");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

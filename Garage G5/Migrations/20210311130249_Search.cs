using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class Search : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_RegistrationNum",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNum",
                table: "ParkedVehicle",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNum",
                table: "ParkedVehicle",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_RegistrationNum",
                table: "ParkedVehicle",
                column: "RegistrationNum",
                unique: true,
                filter: "[RegistrationNum] IS NOT NULL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class isadult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_TypeOfVehicle_TypeOfVehicleId",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<int>(
                name: "TypeOfVehicleId",
                table: "ParkedVehicle",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdult",
                table: "Member",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_TypeOfVehicle_TypeOfVehicleId",
                table: "ParkedVehicle",
                column: "TypeOfVehicleId",
                principalTable: "TypeOfVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_TypeOfVehicle_TypeOfVehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "IsAdult",
                table: "Member");

            migrationBuilder.AlterColumn<int>(
                name: "TypeOfVehicleId",
                table: "ParkedVehicle",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_TypeOfVehicle_TypeOfVehicleId",
                table: "ParkedVehicle",
                column: "TypeOfVehicleId",
                principalTable: "TypeOfVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

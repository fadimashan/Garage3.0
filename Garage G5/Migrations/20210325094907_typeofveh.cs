using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class typeofveh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInGarage",
                table: "ParkedVehicle",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "ParkedVehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfVehicleId",
                table: "ParkedVehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeOfVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraRate = table.Column<int>(type: "int", nullable: false),
                    ExtraHourlyRate = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfVehicle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_TypeOfVehicleId",
                table: "ParkedVehicle",
                column: "TypeOfVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_TypeOfVehicle_TypeOfVehicleId",
                table: "ParkedVehicle",
                column: "TypeOfVehicleId",
                principalTable: "TypeOfVehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_TypeOfVehicle_TypeOfVehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "TypeOfVehicle");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_TypeOfVehicleId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "IsInGarage",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "TypeOfVehicleId",
                table: "ParkedVehicle");
        }
    }
}

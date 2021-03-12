using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class newView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralInfoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    RegistrationNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnteringTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalTimeParked = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralInfoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralInfoModel_1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<int>(type: "int", nullable: false),
                    RegistrationNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnteringTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalTimeParked = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralInfoModel_1", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralInfoModel");

            migrationBuilder.DropTable(
                name: "GeneralInfoModel_1");
        }
    }
}

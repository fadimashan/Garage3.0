using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class PartialView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralInfoModel_1");

            migrationBuilder.DropTable(
                name: "ReceiptModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralInfoModel_1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnteringTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTimeParked = table.Column<TimeSpan>(type: "time", nullable: false),
                    VehicleType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralInfoModel_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnteringTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    RegistrationNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTimeParked = table.Column<TimeSpan>(type: "time", nullable: false),
                    VehicleType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptModel", x => x.Id);
                });
        }
    }
}

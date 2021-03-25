using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage_G5.Migrations
{
    public partial class member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    PersonalIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BonusAccountExpires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_MemberId",
                table: "ParkedVehicle",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_Member_MemberId",
                table: "ParkedVehicle",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_Member_MemberId",
                table: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_MemberId",
                table: "ParkedVehicle");
        }
    }
}

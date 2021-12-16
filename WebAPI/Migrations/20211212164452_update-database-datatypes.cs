using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class updatedatabasedatatypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Plants_PlantEUI",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_PlantEUI",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PlantEUI",
                table: "Measurements");

            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "Plants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Device",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "PlantEUI",
                table: "Measurements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_PlantEUI",
                table: "Measurements",
                column: "PlantEUI");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Plants_PlantEUI",
                table: "Measurements",
                column: "PlantEUI",
                principalTable: "Plants",
                principalColumn: "EUI",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

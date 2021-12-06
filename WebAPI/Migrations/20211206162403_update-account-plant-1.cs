using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class updateaccountplant1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EUI",
                table: "Plants");

            migrationBuilder.AddColumn<string>(
                name: "DeviceIdentifier",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceIdentifier",
                table: "Plants");

            migrationBuilder.AddColumn<int>(
                name: "EUI",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

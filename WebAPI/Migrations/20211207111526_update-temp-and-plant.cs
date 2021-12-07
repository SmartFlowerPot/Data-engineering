using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class updatetempandplant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "Ts",
                table: "Temperatures");

            migrationBuilder.RenameColumn(
                name: "DeviceIdentifier",
                table: "Plants",
                newName: "EUI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EUI",
                table: "Plants",
                newName: "DeviceIdentifier");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Temperatures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Ts",
                table: "Temperatures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}

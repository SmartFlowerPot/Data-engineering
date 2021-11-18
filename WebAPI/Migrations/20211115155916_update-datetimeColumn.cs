using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class updatedatetimeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures");

            migrationBuilder.AddColumn<string>(
                name: "DateTime",
                table: "Temperatures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures",
                column: "DateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Temperatures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperatures",
                table: "Temperatures",
                column: "TimeStamp");
        }
    }
}

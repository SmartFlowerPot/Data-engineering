using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class updatedatabasedatatypes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(50)",
                nullable: true);
            
            
            migrationBuilder.AddColumn<string>(
                name: "AccountUsername",
                table: "Plants",
                type: "nvarchar(50)",
                nullable: true);
            
            migrationBuilder.CreateIndex(
                name: "Plants.IX_Plants_AccountUsername",
                table: "Plants",
                column: "AccountUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Accounts_AccountUsername",
                table: "Plants",
                column: "AccountUsername",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Accounts_AccountUsername",
                table: "Plants");

            migrationBuilder.DropIndex(
                name: "IX_Plants_AccountUsername",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "AccountUsername",
                table: "Plants");
            
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Plants_PlantEUI",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_PlantEUI",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "PlantEUI",
                table: "Measurements");

            migrationBuilder.AlterColumn<string>(
                name: "EUI",
                table: "Plants",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "Plants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}

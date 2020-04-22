using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelQuoteApp.DataLayer.Migrations
{
    public partial class foreignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_IDId",
                table: "Client",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_User_IDId",
                table: "Client",
                column: "User_IDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_UsersInfo_User_IDId",
                table: "Client",
                column: "User_IDId",
                principalTable: "UsersInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_UsersInfo_User_IDId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_User_IDId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "User_IDId",
                table: "Client");
        }
    }
}

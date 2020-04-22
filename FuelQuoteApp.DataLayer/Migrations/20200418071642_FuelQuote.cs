using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelQuoteApp.DataLayer.Migrations
{
    public partial class FuelQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FuelQuote",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GallonsRequested = table.Column<int>(nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    PricePerGallon = table.Column<float>(nullable: false),
                    TotalAmount = table.Column<float>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuelQuote_UsersInfo_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_FuelQuote_UserId",
            //    table: "FuelQuote",
            //    column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuelQuote");
        }
    }
}

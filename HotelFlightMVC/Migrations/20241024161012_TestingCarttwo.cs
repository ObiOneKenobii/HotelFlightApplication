using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelFlightMVC.Migrations
{
    /// <inheritdoc />
    public partial class TestingCarttwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "FlightTickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlightTickets_CartId",
                table: "FlightTickets",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlightTickets_Carts_CartId",
                table: "FlightTickets",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightTickets_Carts_CartId",
                table: "FlightTickets");

            migrationBuilder.DropIndex(
                name: "IX_FlightTickets_CartId",
                table: "FlightTickets");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "FlightTickets");
        }
    }
}

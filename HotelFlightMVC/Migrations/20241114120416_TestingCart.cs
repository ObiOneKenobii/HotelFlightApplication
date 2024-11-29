using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelFlightMVC.Migrations
{
    /// <inheritdoc />
    public partial class TestingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "HotelRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelRooms_CartId",
                table: "HotelRooms",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelRooms_Carts_CartId",
                table: "HotelRooms",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelRooms_Carts_CartId",
                table: "HotelRooms");

            migrationBuilder.DropIndex(
                name: "IX_HotelRooms_CartId",
                table: "HotelRooms");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "HotelRooms");
        }
    }
}

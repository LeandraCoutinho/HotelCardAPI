using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelCard.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateconsumption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumptions_Products_ProductId",
                table: "Consumptions");

            migrationBuilder.DropIndex(
                name: "IX_Consumptions_ProductId",
                table: "Consumptions");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Consumptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Consumptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consumptions_ProductId",
                table: "Consumptions",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumptions_Products_ProductId",
                table: "Consumptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

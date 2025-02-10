using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelCard.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateconsumptionproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalValue",
                table: "ConsumptionProducts",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "ConsumptionProducts");
        }
    }
}

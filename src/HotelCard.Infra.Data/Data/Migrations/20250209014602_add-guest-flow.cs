using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelCard.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class addguestflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuestFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuestId = table.Column<int>(type: "int", nullable: false),
                    AccessAreaId = table.Column<int>(type: "int", nullable: false),
                    AccessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestFlow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestFlow_AccessAreas_AccessAreaId",
                        column: x => x.AccessAreaId,
                        principalTable: "AccessAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestFlow_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GuestFlow_AccessAreaId",
                table: "GuestFlow",
                column: "AccessAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestFlow_GuestId",
                table: "GuestFlow",
                column: "GuestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestFlow");
        }
    }
}

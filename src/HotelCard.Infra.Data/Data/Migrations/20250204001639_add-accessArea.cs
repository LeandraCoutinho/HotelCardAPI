using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelCard.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class addaccessArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccessAreas",
                columns: new[] { "Name", "PhotoUrl", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "Restaurante", "https://images.app.goo.gl/9iddAgUySYAEFgM89", DateTime.Now, DateTime.Now },
                    { "Academia", "https://images.app.goo.gl/YbFnau4tzoKB55RaA", DateTime.Now, DateTime.Now },
                    { "Sala de Jogos", "https://images.app.goo.gl/zVJJtbuMHkVvej7s8", DateTime.Now, DateTime.Now },
                    { "Área Kids", "https://images.app.goo.gl/Z4ySWibwt6fywzTTA", DateTime.Now, DateTime.Now },
                    { "Área de Lazer", "https://images.app.goo.gl/dBqbuQEtDrS5ufNcA", DateTime.Now, DateTime.Now },
                    { "Piscina", "https://images.app.goo.gl/SP1VQM9ZHQEueiw76", DateTime.Now, DateTime.Now },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

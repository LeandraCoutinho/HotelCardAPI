using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelCard.Infra.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class addproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Name", "Price", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "Água Mineral", 2.50, DateTime.Now, DateTime.Now },
                    { "Refrigerante Lata", 5.00, DateTime.Now, DateTime.Now },
                    { "Suco Natural", 7.00, DateTime.Now, DateTime.Now },
                    { "Café Expresso", 4.50, DateTime.Now, DateTime.Now },
                    { "Vinho Suave", 33.50, DateTime.Now, DateTime.Now },
                    { "Heineken", 7.50, DateTime.Now, DateTime.Now },
                    { "Sanduíche Natural", 12.00, DateTime.Now, DateTime.Now },
                    { "Salada Caesar", 18.00, DateTime.Now, DateTime.Now },
                    { "Prato Executivo (Carne)", 25.00, DateTime.Now, DateTime.Now },
                    { "Prato Executivo (Frango)", 23.00, DateTime.Now, DateTime.Now },
                    { "Prato Executivo (Vegetariano)", 22.00, DateTime.Now, DateTime.Now },
                    { "Sobremesa (Pudim)", 8.00, DateTime.Now, DateTime.Now },
                    { "Sobremesa (Mousse de Chocolate)", 9.00, DateTime.Now, DateTime.Now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Product");
        }
    }
}

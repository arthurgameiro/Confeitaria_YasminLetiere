using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoverProdutosNaoUtilizados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e2222222-2222-2222-2222-222222222222"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "SeasonalTag", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b2222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), "Doce fino de nozes selecionadas trituradas, banhado em glaçúcar fondant e decorado com uma noz inteira por cima.", "/images/camafeu_nozes.jpg", true, "Camafeu de Nozes Real", 7.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c1111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), "Camadas de creme de avelã trufado, chocolate branco Laka, brigadeiro gourmet, morangos e finalizado com Kinder Bueno.", "/images/kinder_cup.jpg", true, "Copo da Felicidade Kinder Bueno", 28.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), "Guirlanda decorada composta por 25 brigadeiros gourmets nos sabores tradicional belga, ninho com nutella, pistache e nozes.", "/images/christmas_wreath.jpg", true, "Guirlanda Festiva de Brigadeiros", 120.00m, "Natal", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}

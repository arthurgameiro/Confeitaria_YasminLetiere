using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMaisSazonalidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sazonalidades",
                columns: new[] { "Id", "AtualizadoEm", "DataFim", "DataInicio", "MensagemExpirada", "Nome" },
                values: new object[,]
                {
                    { new Guid("57777777-7777-7777-7777-777777777777"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 3, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "O carnaval acabou, mas os doces ficam! As encomendas de Carnaval já encerraram. Feliz Carnaval e até o próximo ano! 🎭", "Carnaval" },
                    { new Guid("58888888-8888-8888-8888-888888888888"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 9, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas do Dia dos Pais foram encerradas. Obrigada pela preferência! Que todos os pais se sintam especiais e celebrados.", "Dia dos Pais" },
                    { new Guid("59999999-9999-9999-9999-999999999999"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 10, 12, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas do Dia das Crianças foram encerradas! Que a alegria das crianças dure o ano todo. Até a próxima! 🎈", "Dia das Crianças" },
                    { new Guid("5aaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2027, 1, 2, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas de Réveillon foram encerradas! Que o Ano Novo seja cheio de doçura e realizações. Feliz 2027! 🥂", "Réveillon" },
                    { new Guid("5bbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 8, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas do Dia da Mulher foram encerradas. Obrigada por celebrar conosco! Que todas as mulheres se sintam especiais todos os dias. 💐", "Dia da Mulher" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("57777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("58888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("59999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("5aaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("5bbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));
        }
    }
}

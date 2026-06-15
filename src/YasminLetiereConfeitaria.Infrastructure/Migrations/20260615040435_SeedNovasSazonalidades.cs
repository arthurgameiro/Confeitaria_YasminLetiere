using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedNovasSazonalidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Sazonalidades");

            migrationBuilder.InsertData(
                table: "Sazonalidades",
                columns: new[] { "Id", "AtualizadoEm", "DataFim", "DataInicio", "MensagemExpirada", "Nome" },
                values: new object[,]
                {
                    { new Guid("54444444-4444-4444-4444-444444444444"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 30, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Arraiá encerrado! As encomendas da Festa Junina já se encerraram. Que a festa continue no coração! Nos vemos no próximo arraiá. 🎉", "Festa Junina" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 5, 10, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas do Dia das Mães foram encerradas. Obrigada pela preferência! Que todas as mães se sintam amadas e celebradas.", "Dia das Mães" },
                    { new Guid("56666666-6666-6666-6666-666666666666"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 10, 31, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas de Halloween foram encerradas! Esperamos ter adoçado o seu outubro. Até o próximo susto! 🎃", "Halloween" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("54444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("56666666-6666-6666-6666-666666666666"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Sazonalidades",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-111111111111"),
                column: "CriadoEm",
                value: new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("52222222-2222-2222-2222-222222222222"),
                column: "CriadoEm",
                value: new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("53333333-3333-3333-3333-333333333333"),
                column: "CriadoEm",
                value: new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}

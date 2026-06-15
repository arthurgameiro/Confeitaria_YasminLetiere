using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarDepoimentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depoimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeCliente = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Texto = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Nota = table.Column<int>(type: "integer", nullable: false),
                    Fonte = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    DataDepoimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depoimentos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Depoimentos",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "DataDepoimento", "Fonte", "NomeCliente", "Nota", "Ordem", "Texto" },
                values: new object[,]
                {
                    { new Guid("dd111111-1111-1111-1111-111111111111"), true, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Google", "Melissa Marcial Cecilio", 5, 1, "Sou cliente assídua da Yasmin. Já experimentei várias de suas criações e amo. O mais legal é que ela está sempre aberta a críticas construtivas e sugestões de melhoria. As fotos que adicionei são de todas as delícias feitas por ela que eu comi!" },
                    { new Guid("dd222222-2222-2222-2222-222222222222"), true, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Google", "Jaqueline Mangabeira", 5, 2, "Um dos melhores bolos de aniversário que já tive! Bolo lindo com tema galáxia e muuuito gostoso. Eu escolhi a massa de limão com blueberry e recheio de ninho e foi a melhor escolha! Os meus convidados amaram." },
                    { new Guid("dd333333-3333-3333-3333-333333333333"), true, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Google", "Tássia Pinheiro", 5, 3, "O bolo de brigadeiro com caramelo salgado é maravilhoso! Arrisco a dizer que é o melhor bolo que já comi na vida. O atendimento da Yasmin também é um diferencial. Sempre muito gentil e simpática e tira todas as dúvidas." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depoimentos");
        }
    }
}

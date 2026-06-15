using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRedesSociaisEConfiguracoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracoesSistema",
                columns: table => new
                {
                    Chave = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesSistema", x => x.Chave);
                });

            migrationBuilder.CreateTable(
                name: "RedesSociais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Icone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    Ordem = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociais", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ConfiguracoesSistema",
                columns: new[] { "Chave", "AtualizadoEm", "Valor" },
                values: new object[] { "catalogo_sazonal_ativo", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "true" });

            migrationBuilder.InsertData(
                table: "RedesSociais",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "Icone", "Nome", "Ordem", "Url" },
                values: new object[,]
                {
                    { new Guid("ee111111-1111-1111-1111-111111111111"), true, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "chat", "WhatsApp", 1, "https://wa.me/message/3DAIKNWMQVTAM1" },
                    { new Guid("ee222222-2222-2222-2222-222222222222"), true, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "photo_camera", "Instagram", 2, "https://instagram.com/yasminletiereconfeitaria" },
                    { new Guid("ee333333-3333-3333-3333-333333333333"), false, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "play_circle", "TikTok", 3, "https://tiktok.com/@yasminletiereconfeitaria" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracoesSistema");

            migrationBuilder.DropTable(
                name: "RedesSociais");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFacebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee333333-3333-3333-3333-333333333333"),
                columns: new[] { "Ativo", "Icone", "Nome", "Url" },
                values: new object[] { true, "thumb_up", "Facebook", "https://www.facebook.com/yasminletieredoces" });

            migrationBuilder.InsertData(
                table: "RedesSociais",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "Icone", "Nome", "Ordem", "Url" },
                values: new object[] { new Guid("ee444444-4444-4444-4444-444444444444"), false, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "play_circle", "TikTok", 4, "https://tiktok.com/@yasminletiereconfeitaria" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee444444-4444-4444-4444-444444444444"));

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee333333-3333-3333-3333-333333333333"),
                columns: new[] { "Ativo", "Icone", "Nome", "Url" },
                values: new object[] { false, "play_circle", "TikTok", "https://tiktok.com/@yasminletiereconfeitaria" });
        }
    }
}

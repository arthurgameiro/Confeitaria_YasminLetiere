using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarIconesRedesSociais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee111111-1111-1111-1111-111111111111"),
                column: "Icone",
                value: "fa-brands fa-whatsapp");

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee222222-2222-2222-2222-222222222222"),
                column: "Icone",
                value: "fa-brands fa-instagram");

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee333333-3333-3333-3333-333333333333"),
                column: "Icone",
                value: "fa-brands fa-facebook-f");

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee444444-4444-4444-4444-444444444444"),
                column: "Icone",
                value: "fa-brands fa-tiktok");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee111111-1111-1111-1111-111111111111"),
                column: "Icone",
                value: "chat");

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee222222-2222-2222-2222-222222222222"),
                column: "Icone",
                value: "photo_camera");

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee333333-3333-3333-3333-333333333333"),
                column: "Icone",
                value: "thumb_up");

            migrationBuilder.UpdateData(
                table: "RedesSociais",
                keyColumn: "Id",
                keyValue: new Guid("ee444444-4444-4444-4444-444444444444"),
                column: "Icone",
                value: "play_circle");
        }
    }
}

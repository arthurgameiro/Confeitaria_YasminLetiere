using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarIconeSazonalidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icone",
                table: "Sazonalidades",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-111111111111"),
                column: "Icone",
                value: "🐇");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("52222222-2222-2222-2222-222222222222"),
                column: "Icone",
                value: "🎄");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("53333333-3333-3333-3333-333333333333"),
                column: "Icone",
                value: "💖");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("54444444-4444-4444-4444-444444444444"),
                column: "Icone",
                value: "🌽");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Icone",
                value: "👩‍👦");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("56666666-6666-6666-6666-666666666666"),
                column: "Icone",
                value: "🎃");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("57777777-7777-7777-7777-777777777777"),
                column: "Icone",
                value: "🎭");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("58888888-8888-8888-8888-888888888888"),
                column: "Icone",
                value: "👨‍👦");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("59999999-9999-9999-9999-999999999999"),
                column: "Icone",
                value: "🎈");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("5aaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "Icone",
                value: "🥂");

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("5bbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Icone",
                value: "💐");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icone",
                table: "Sazonalidades");
        }
    }
}

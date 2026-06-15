using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarAtivoSazonalidade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Sazonalidades",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-111111111111"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("52222222-2222-2222-2222-222222222222"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("53333333-3333-3333-3333-333333333333"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("54444444-4444-4444-4444-444444444444"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("56666666-6666-6666-6666-666666666666"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("57777777-7777-7777-7777-777777777777"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("58888888-8888-8888-8888-888888888888"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("59999999-9999-9999-9999-999999999999"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("5aaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Sazonalidades",
                keyColumn: "Id",
                keyValue: new Guid("5bbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "Ativo",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Sazonalidades");
        }
    }
}

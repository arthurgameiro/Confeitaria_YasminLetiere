using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirPlaceholderImagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Products\" SET \"ImageUrl\" = '/images/sem_foto.jpg' WHERE \"ImageUrl\" = '/images/placeholder.jpg' OR \"ImageUrl\" IS NULL OR \"ImageUrl\" = '';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE \"Products\" SET \"ImageUrl\" = '/images/placeholder.jpg' WHERE \"ImageUrl\" = '/images/sem_foto.jpg';");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YasminLetiereConfeitaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Icon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sazonalidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MensagemExpirada = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sazonalidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SeasonalTag = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Icon", "Name", "Order" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Bolos decorados e com sabores inesquecíveis para celebrar.", "cake", "Bolos Festivos", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Doces finos e tradicionais feitos com ingredientes nobres.", "cookie", "Docinhos Gourmet", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Sobremesas cremosas em camadas para adoçar qualquer dia.", "local_bar", "Copos da Felicidade", 3 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Empadões caseiros, quiches e tortas salgadas para sua festa ou ceia.", "restaurant", "Salgados", 4 }
                });

            migrationBuilder.InsertData(
                table: "Sazonalidades",
                columns: new[] { "Id", "AtualizadoEm", "CriadoEm", "DataFim", "DataInicio", "MensagemExpirada", "Nome" },
                values: new object[,]
                {
                    { new Guid("51111111-1111-1111-1111-111111111111"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 15, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "As encomendas de Páscoa deste ano foram encerradas! Agradecemos imensamente o seu interesse. Quem sabe na próxima temporada?", "Páscoa" },
                    { new Guid("52222222-2222-2222-2222-222222222222"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 12, 25, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Nossas vagas para as encomendas de Natal já estão esgotadas! Boas Festas, nos vemos na próxima ceia!", "Natal" },
                    { new Guid("53333333-3333-3333-3333-333333333333"), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 12, 23, 59, 59, 0, DateTimeKind.Utc), new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Os pedidos para o Dia dos Namorados estão encerrados. Agradecemos a preferência, quem sabe no ano que vem?", "Namorados" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("99999999-9999-9999-9999-999999999999"), "7501296317e7e875d8cbf01972c9cc279b993b4b1e04d342908a0ffbb1ab78cf", "Admin", "yasmin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "SeasonalTag", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a1111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "Massa vermelha aveludada com cacau premium, recheada com mousse cremoso à base de cream cheese e geleia artesanal de frutas vermelhas.", "/images/red_velvet.jpg", true, "Bolo Red Velvet Clássico", 160.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111"), "Massa de chocolate nobre, recheio duplo de brigadeiro belga trufado gourmet e morangos frescos selecionados.", "/images/bolo_brigadeiro_duo.jpg", true, "Bolo Belga Trufado com Morango", 180.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a3333333-3333-3333-3333-333333333333"), new Guid("11111111-1111-1111-1111-111111111111"), "Massa amanteigada premium com recheio de creme trufado, finamente espatulada em buttercream, decorada com fatias de laranja desidratada, morangos frescos e delicados ramos de eucalipto.", "/images/bolo_artesanal_laranja.jpg", true, "Bolo Espatulado Decorado", 240.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a4444444-4444-4444-4444-444444444444"), new Guid("11111111-1111-1111-1111-111111111111"), "Monte o seu bolo! Escolha o tamanho, recheio e adicionais. O valor é calculado na hora do pedido.", "/images/bolo_brigadeiro_duo.jpg", true, "Naked Cake Personalizado", 180.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b1111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), "Brigadeiro super cremoso de pistache siciliano puro, envolto em pistache picado e finalizado com ganache.", "/images/pistachio_brigadeiro.jpg", true, "Brigadeiro de Pistache Siciliano", 6.50m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b2222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), "Doce fino de nozes selecionadas trituradas, banhado em glaçúcar fondant e decorado com uma noz inteira por cima.", "/images/camafeu_nozes.jpg", true, "Camafeu de Nozes Real", 7.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b3333333-3333-3333-3333-333333333333"), new Guid("22222222-2222-2222-2222-222222222222"), "Fatia generosa de bolo de cookie crocante por fora e macio por dentro, recheada com ganache cremosa de chocolate ao leite e Nutella premium.", "/images/cookie_cake.jpg", true, "Fatia de Cookie Cake Recheado", 16.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c1111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), "Camadas de creme de avelã trufado, chocolate branco Laka, brigadeiro gourmet, morangos e finalizado com Kinder Bueno.", "/images/kinder_cup.jpg", true, "Copo da Felicidade Kinder Bueno", 28.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d1111111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), "Casca de chocolate ao leite recheada com camadas de brigadeiro de leite Ninho cremoso, pedaços de brownie úmido e brigadeiros gourmet.", "/images/easter_egg_brownie.jpg", true, "Ovo de Colher Brownie com Ninho", 85.00m, "Páscoa", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d2222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), "Casca dupla de chocolate belga recheada com ganache trufada de pistache artesanal.", "/images/easter_egg_pistachio.jpg", true, "Ovo Trufado de Pistache", 110.00m, "Páscoa", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e1111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "Chocotone artesanal de fermentação natural, super recheado com brigadeiro de chocolate belga trufado e cobertura de chocolate meio amargo com granulados nobres.", "/images/christmas_chocotone.jpg", true, "Chocotone Trufado de Chocolate Belga", 95.00m, "Natal", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e2222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), "Guirlanda decorada composta por 25 brigadeiros gourmets nos sabores tradicional belga, ninho com nutella, pistache e nozes.", "/images/christmas_wreath.jpg", true, "Guirlanda Festiva de Brigadeiros", 120.00m, "Natal", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f1111111-1111-1111-1111-111111111111"), new Guid("44444444-4444-4444-4444-444444444444"), "Empadão tamanho família com massa levemente folhada que derrete na boca, super recheado com frango cremoso desfiado e requeijão.", "/images/empadao_frango.jpg", true, "Empadão Cremoso de Frango", 85.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f2222222-2222-2222-2222-222222222222"), new Guid("44444444-4444-4444-4444-444444444444"), "Massa crocante e recheio cremoso à base de creme de leite fresco, alho-poró salteado na manteiga e cubos crocantes de bacon premium.", "/images/quiche_alho_poro.jpg", true, "Quiche de Alho Poró com Bacon", 95.00m, null, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f3333333-3333-3333-3333-333333333333"), new Guid("44444444-4444-4444-4444-444444444444"), "Folhado crocante artesanal recheado com lombo suíno assado desfiado, geleia suave de ameixa e queijo provolone. Perfeito para o Natal.", "/images/folhado_natal.jpg", true, "Folhado Festivo de Lombo com Ameixa", 110.00m, "Natal", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sazonalidades_Nome",
                table: "Sazonalidades",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sazonalidades");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

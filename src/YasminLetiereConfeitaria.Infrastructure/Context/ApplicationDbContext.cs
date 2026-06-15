using Microsoft.EntityFrameworkCore;
using System;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sazonalidade> Sazonalidades { get; set; }
        public DbSet<FeiraGastronomica> FeirasGastronomicas { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        public DbSet<ConfiguracaoSistema> ConfiguracoesSistema { get; set; }
        public DbSet<Depoimento> Depoimentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração das entidades
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Description).HasMaxLength(250);
                entity.Property(c => c.Icon).HasMaxLength(50);
                entity.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.ImageUrl).HasMaxLength(250);
                entity.Property(p => p.SeasonalTag).HasMaxLength(50);
            });

            modelBuilder.Entity<Sazonalidade>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Nome).IsRequired().HasMaxLength(100);
                entity.HasIndex(s => s.Nome).IsUnique();
                entity.Property(s => s.MensagemExpirada).HasMaxLength(500);
                entity.Property(s => s.Icone).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).HasMaxLength(20);
            });

            modelBuilder.Entity<FeiraGastronomica>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Nome).IsRequired().HasMaxLength(150);
                entity.Property(f => f.Local).IsRequired().HasMaxLength(250);
                entity.Property(f => f.Descricao).HasMaxLength(500);
            });

            modelBuilder.Entity<RedeSocial>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Nome).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Url).IsRequired().HasMaxLength(500);
                entity.Property(r => r.Icone).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ConfiguracaoSistema>(entity =>
            {
                entity.HasKey(c => c.Chave);
                entity.Property(c => c.Chave).HasMaxLength(100);
                entity.Property(c => c.Valor).IsRequired().HasMaxLength(500);
            });

            modelBuilder.Entity<Depoimento>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.NomeCliente).IsRequired().HasMaxLength(150);
                entity.Property(d => d.Texto).IsRequired().HasMaxLength(2000);
                entity.Property(d => d.Fonte).HasMaxLength(50);
            });

            // Seed de Categorias Padrões
            var catBolosId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var catDocesId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var catCoposId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var catSalgadosId = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var seedDateTime = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Category>().HasData(
                new { Id = catBolosId, Name = "Bolos Festivos", Description = "Bolos decorados e com sabores inesquecíveis para celebrar.", Icon = "cake", Order = 1 },
                new { Id = catDocesId, Name = "Docinhos Gourmet", Description = "Doces finos e tradicionais feitos com ingredientes nobres.", Icon = "cookie", Order = 2 },
                new { Id = catCoposId, Name = "Copos da Felicidade", Description = "Sobremesas cremosas em camadas para adoçar qualquer dia.", Icon = "local_bar", Order = 3 },
                new { Id = catSalgadosId, Name = "Salgados", Description = "Empadões caseiros, quiches e tortas salgadas para sua festa ou ceia.", Icon = "restaurant", Order = 4 }
            );

            // Seed de Produtos Padrões (Cardápio Fixo)
            modelBuilder.Entity<Product>().HasData(
                new
                {
                    Id = Guid.Parse("a1111111-1111-1111-1111-111111111111"),
                    Name = "Bolo Red Velvet Clássico",
                    Description = "Massa vermelha aveludada com cacau premium, recheada com mousse cremoso à base de cream cheese e geleia artesanal de frutas vermelhas.",
                    Price = 160.00m,
                    ImageUrl = "/images/red_velvet.jpg",
                    IsAvailable = true,
                    CategoryId = catBolosId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("a2222222-2222-2222-2222-222222222222"),
                    Name = "Bolo Belga Trufado com Morango",
                    Description = "Massa de chocolate nobre, recheio duplo de brigadeiro belga trufado gourmet e morangos frescos selecionados.",
                    Price = 180.00m,
                    ImageUrl = "/images/bolo_brigadeiro_duo.jpg",
                    IsAvailable = true,
                    CategoryId = catBolosId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("b1111111-1111-1111-1111-111111111111"),
                    Name = "Brigadeiro de Pistache Siciliano",
                    Description = "Brigadeiro super cremoso de pistache siciliano puro, envolto em pistache picado e finalizado com ganache.",
                    Price = 6.50m,
                    ImageUrl = "/images/pistachio_brigadeiro.jpg",
                    IsAvailable = true,
                    CategoryId = catDocesId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },

                new
                {
                    Id = Guid.Parse("a3333333-3333-3333-3333-333333333333"),
                    Name = "Bolo Espatulado Decorado",
                    Description = "Massa amanteigada premium com recheio de creme trufado, finamente espatulada em buttercream, decorada com fatias de laranja desidratada, morangos frescos e delicados ramos de eucalipto.",
                    Price = 240.00m,
                    ImageUrl = "/images/bolo_artesanal_laranja.jpg",
                    IsAvailable = true,
                    CategoryId = catBolosId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("b3333333-3333-3333-3333-333333333333"),
                    Name = "Fatia de Cookie Cake Recheado",
                    Description = "Fatia generosa de bolo de cookie crocante por fora e macio por dentro, recheada com ganache cremosa de chocolate ao leite e Nutella premium.",
                    Price = 16.00m,
                    ImageUrl = "/images/cookie_cake.jpg",
                    IsAvailable = true,
                    CategoryId = catDocesId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("a4444444-4444-4444-4444-444444444444"),
                    Name = "Naked Cake Personalizado",
                    Description = "Monte o seu bolo! Escolha o tamanho, recheio e adicionais. O valor é calculado na hora do pedido.",
                    Price = 180.00m,
                    ImageUrl = "/images/bolo_brigadeiro_duo.jpg",
                    IsAvailable = true,
                    CategoryId = catBolosId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("f1111111-1111-1111-1111-111111111111"),
                    Name = "Empadão Cremoso de Frango",
                    Description = "Empadão tamanho família com massa levemente folhada que derrete na boca, super recheado com frango cremoso desfiado e requeijão.",
                    Price = 85.00m,
                    ImageUrl = "/images/empadao_frango.jpg",
                    IsAvailable = true,
                    CategoryId = catSalgadosId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("f2222222-2222-2222-2222-222222222222"),
                    Name = "Quiche de Alho Poró com Bacon",
                    Description = "Massa crocante e recheio cremoso à base de creme de leite fresco, alho-poró salteado na manteiga e cubos crocantes de bacon premium.",
                    Price = 95.00m,
                    ImageUrl = "/images/quiche_alho_poro.jpg",
                    IsAvailable = true,
                    CategoryId = catSalgadosId,
                    SeasonalTag = (string?)null,
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                }
            );

            // Seed de Produtos Padrões (Cardápio Sazonal)
            modelBuilder.Entity<Product>().HasData(
                // Páscoa
                new
                {
                    Id = Guid.Parse("d1111111-1111-1111-1111-111111111111"),
                    Name = "Ovo de Colher Brownie com Ninho",
                    Description = "Casca de chocolate ao leite recheada com camadas de brigadeiro de leite Ninho cremoso, pedaços de brownie úmido e brigadeiros gourmet.",
                    Price = 85.00m,
                    ImageUrl = "/images/easter_egg_brownie.jpg",
                    IsAvailable = true,
                    CategoryId = catDocesId,
                    SeasonalTag = "Páscoa",
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                new
                {
                    Id = Guid.Parse("d2222222-2222-2222-2222-222222222222"),
                    Name = "Ovo Trufado de Pistache",
                    Description = "Casca dupla de chocolate belga recheada com ganache trufada de pistache artesanal.",
                    Price = 110.00m,
                    ImageUrl = "/images/easter_egg_pistachio.jpg",
                    IsAvailable = true,
                    CategoryId = catDocesId,
                    SeasonalTag = "Páscoa",
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },
                // Natal
                new
                {
                    Id = Guid.Parse("e1111111-1111-1111-1111-111111111111"),
                    Name = "Chocotone Trufado de Chocolate Belga",
                    Description = "Chocotone artesanal de fermentação natural, super recheado com brigadeiro de chocolate belga trufado e cobertura de chocolate meio amargo com granulados nobres.",
                    Price = 95.00m,
                    ImageUrl = "/images/christmas_chocotone.jpg",
                    IsAvailable = true,
                    CategoryId = catBolosId,
                    SeasonalTag = "Natal",
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                },

                new
                {
                    Id = Guid.Parse("f3333333-3333-3333-3333-333333333333"),
                    Name = "Folhado Festivo de Lombo com Ameixa",
                    Description = "Folhado crocante artesanal recheado com lombo suíno assado desfiado, geleia suave de ameixa e queijo provolone. Perfeito para o Natal.",
                    Price = 110.00m,
                    ImageUrl = "/images/folhado_natal.jpg",
                    IsAvailable = true,
                    CategoryId = catSalgadosId,
                    SeasonalTag = "Natal",
                    CreatedAt = seedDateTime,
                    UpdatedAt = seedDateTime
                }
            );

            // Seed do Usuário Administrativo Padrão: yasmin
            // Senha padrão hash para "doce123" usando uma hash SHA256 simples
            // Hash SHA256 de "doce123" concatenada com um salt fixo ou apenas hash direta para facilidade.
            // Para segurança e simplicidade, vamos usar "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92" (SHA256 direta para "123456").
            // Vamos gerar a senha SHA256 para "yasmin123":
            // "yasmin123" -> SHA256 = "7501296317e7e875d8cbf01972c9cc279b993b4b1e04d342908a0ffbb1ab78cf"
            modelBuilder.Entity<User>().HasData(
                new
                {
                    Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    Username = "yasmin",
                    PasswordHash = "7501296317e7e875d8cbf01972c9cc279b993b4b1e04d342908a0ffbb1ab78cf", // Senha: yasmin123
                    Role = "Admin",
                    CreatedAt = seedDateTime
                }
            );

            // Seed de Sazonalidades
            modelBuilder.Entity<Sazonalidade>().HasData(
                new
                {
                    Id = Guid.Parse("51111111-1111-1111-1111-111111111111"),
                    Nome = "Páscoa",
                    DataInicio = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 4, 15, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas de Páscoa deste ano foram encerradas! Agradecemos imensamente o seu interesse. Quem sabe na próxima temporada?",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🐇",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("52222222-2222-2222-2222-222222222222"),
                    Nome = "Natal",
                    DataInicio = new DateTime(2026, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 12, 25, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "Nossas vagas para as encomendas de Natal já estão esgotadas! Boas Festas, nos vemos na próxima ceia!",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🎄",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("53333333-3333-3333-3333-333333333333"),
                    Nome = "Namorados",
                    DataInicio = new DateTime(2026, 5, 20, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 6, 12, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "Os pedidos para o Dia dos Namorados estão encerrados. Agradecemos a preferência, quem sabe no ano que vem?",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "💖",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("54444444-4444-4444-4444-444444444444"),
                    Nome = "Festa Junina",
                    DataInicio = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 6, 30, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "Arraiá encerrado! As encomendas da Festa Junina já se encerraram. Que a festa continue no coração! Nos vemos no próximo arraiá. 🎉",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🌽",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    Nome = "Dia das Mães",
                    DataInicio = new DateTime(2026, 4, 20, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 5, 10, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas do Dia das Mães foram encerradas. Obrigada pela preferência! Que todas as mães se sintam amadas e celebradas.",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "👩‍👦",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("56666666-6666-6666-6666-666666666666"),
                    Nome = "Halloween",
                    DataInicio = new DateTime(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 10, 31, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas de Halloween foram encerradas! Esperamos ter adoçado o seu outubro. Até o próximo susto! 🎃",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🎃",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("57777777-7777-7777-7777-777777777777"),
                    Nome = "Carnaval",
                    DataInicio = new DateTime(2026, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 3, 3, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "O carnaval acabou, mas os doces ficam! As encomendas de Carnaval já encerraram. Feliz Carnaval e até o próximo ano! 🎭",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🎭",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("58888888-8888-8888-8888-888888888888"),
                    Nome = "Dia dos Pais",
                    DataInicio = new DateTime(2026, 7, 20, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 8, 9, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas do Dia dos Pais foram encerradas. Obrigada pela preferência! Que todos os pais se sintam especiais e celebrados.",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "👨‍👦",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("59999999-9999-9999-9999-999999999999"),
                    Nome = "Dia das Crianças",
                    DataInicio = new DateTime(2026, 9, 15, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 10, 12, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas do Dia das Crianças foram encerradas! Que a alegria das crianças dure o ano todo. Até a próxima! 🎈",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🎈",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("5aaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Nome = "Réveillon",
                    DataInicio = new DateTime(2026, 12, 10, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2027, 1, 2, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas de Réveillon foram encerradas! Que o Ano Novo seja cheio de doçura e realizações. Feliz 2027! 🥂",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "🥂",
                    Ativo = true
                },
                new
                {
                    Id = Guid.Parse("5bbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Nome = "Dia da Mulher",
                    DataInicio = new DateTime(2026, 2, 25, 0, 0, 0, DateTimeKind.Utc),
                    DataFim = new DateTime(2026, 3, 8, 23, 59, 59, DateTimeKind.Utc),
                    MensagemExpirada = "As encomendas do Dia da Mulher foram encerradas. Obrigada por celebrar conosco! Que todas as mulheres se sintam especiais todos os dias. 💐",
                    CriadoEm = seedDateTime,
                    AtualizadoEm = seedDateTime,
                    Icone = "💐",
                    Ativo = true
                }
            );

            // Seed de Redes Sociais
            var seedDateTimeRedes = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<RedeSocial>().HasData(
                new
                {
                    Id = Guid.Parse("ee111111-1111-1111-1111-111111111111"),
                    Nome = "WhatsApp",
                    Url = "https://wa.me/message/3DAIKNWMQVTAM1",
                    Icone = "fa-brands fa-whatsapp",
                    Ativo = true,
                    Ordem = 1,
                    CriadoEm = seedDateTimeRedes,
                    AtualizadoEm = seedDateTimeRedes
                },
                new
                {
                    Id = Guid.Parse("ee222222-2222-2222-2222-222222222222"),
                    Nome = "Instagram",
                    Url = "https://instagram.com/yasminletiereconfeitaria",
                    Icone = "fa-brands fa-instagram",
                    Ativo = true,
                    Ordem = 2,
                    CriadoEm = seedDateTimeRedes,
                    AtualizadoEm = seedDateTimeRedes
                },
                new
                {
                    Id = Guid.Parse("ee333333-3333-3333-3333-333333333333"),
                    Nome = "Facebook",
                    Url = "https://www.facebook.com/yasminletieredoces",
                    Icone = "fa-brands fa-facebook-f",
                    Ativo = true,
                    Ordem = 3,
                    CriadoEm = seedDateTimeRedes,
                    AtualizadoEm = seedDateTimeRedes
                },
                new
                {
                    Id = Guid.Parse("ee444444-4444-4444-4444-444444444444"),
                    Nome = "TikTok",
                    Url = "https://tiktok.com/@yasminletiereconfeitaria",
                    Icone = "fa-brands fa-tiktok",
                    Ativo = false,
                    Ordem = 4,
                    CriadoEm = seedDateTimeRedes,
                    AtualizadoEm = seedDateTimeRedes
                }
            );

            // Seed de Configurações do Sistema
            modelBuilder.Entity<ConfiguracaoSistema>().HasData(
                new { Chave = "catalogo_sazonal_ativo", Valor = "true", AtualizadoEm = seedDateTimeRedes }
            );

            // Seed de Depoimentos (Avaliações reais do Google)
            var seedDep = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<Depoimento>().HasData(
                new
                {
                    Id = Guid.Parse("dd111111-1111-1111-1111-111111111111"),
                    NomeCliente = "Melissa Marcial Cecilio",
                    Texto = "Sou cliente assídua da Yasmin. Já experimentei várias de suas criações e amo. O mais legal é que ela está sempre aberta a críticas construtivas e sugestões de melhoria. As fotos que adicionei são de todas as delícias feitas por ela que eu comi!",
                    Nota = 5,
                    Fonte = "Google",
                    Ativo = true,
                    Ordem = 1,
                    DataDepoimento = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    CriadoEm = seedDep,
                    AtualizadoEm = seedDep
                },
                new
                {
                    Id = Guid.Parse("dd222222-2222-2222-2222-222222222222"),
                    NomeCliente = "Jaqueline Mangabeira",
                    Texto = "Um dos melhores bolos de aniversário que já tive! Bolo lindo com tema galáxia e muuuito gostoso. Eu escolhi a massa de limão com blueberry e recheio de ninho e foi a melhor escolha! Os meus convidados amaram.",
                    Nota = 5,
                    Fonte = "Google",
                    Ativo = true,
                    Ordem = 2,
                    DataDepoimento = new DateTime(2023, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                    CriadoEm = seedDep,
                    AtualizadoEm = seedDep
                },
                new
                {
                    Id = Guid.Parse("dd333333-3333-3333-3333-333333333333"),
                    NomeCliente = "Tássia Pinheiro",
                    Texto = "O bolo de brigadeiro com caramelo salgado é maravilhoso! Arrisco a dizer que é o melhor bolo que já comi na vida. O atendimento da Yasmin também é um diferencial. Sempre muito gentil e simpática e tira todas as dúvidas.",
                    Nota = 5,
                    Fonte = "Google",
                    Ativo = true,
                    Ordem = 3,
                    DataDepoimento = new DateTime(2023, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                    CriadoEm = seedDep,
                    AtualizadoEm = seedDep
                }
            );
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;
using YasminLetiereConfeitaria.Infrastructure.Repositories;
using YasminLetiereConfeitaria.Infrastructure.Security;
using YasminLetiereConfeitaria.Application.Interfaces;
using YasminLetiereConfeitaria.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddApplicationPart(typeof(YasminLetiereConfeitaria.Presentation.HomeController).Assembly);

// Database Context (PostgreSQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("YasminLetiereConfeitaria.Infrastructure")));

// Dependency Injection
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISazonalidadeRepository, SazonalidadeRepository>();
builder.Services.AddScoped<IFeiraGastronomicaRepository, FeiraGastronomicaRepository>();
builder.Services.AddScoped<IRedeSocialRepository, RedeSocialRepository>();
builder.Services.AddScoped<IConfiguracaoSistemaRepository, ConfiguracaoSistemaRepository>();
builder.Services.AddScoped<IDepoimentoRepository, DepoimentoRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthAppService, AuthAppService>();

// JWT Authentication Setup
var secretKey = builder.Configuration["JwtSettings:SecretKey"] ?? "YasminLetiereConfeitariaMegaSecretKey2026!!!";
var issuer = builder.Configuration["JwtSettings:Issuer"] ?? "YasminLetiereConfeitaria";
var audience = builder.Configuration["JwtSettings:Audience"] ?? "YasminLetiereConfeitaria";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Extrai o token do cookie HttpOnly seguro
            context.Token = context.Request.Cookies["jwt"];
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            // Intercepta a falha e redireciona para a tela de login ao invés de retornar 401
            context.HandleResponse();
            context.Response.Redirect("/auth/login");
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita Autenticação e Autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Auto-migration at startup for ease of deployment
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações automáticas do PostgreSQL.");
    }
}

app.Run();

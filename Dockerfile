# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copiar arquivos de projeto e restaurar dependências (otimização de cache do Docker)
COPY YasminLetiereConfeitaria.slnx ./
COPY src/YasminLetiereConfeitaria.Domain/YasminLetiereConfeitaria.Domain.csproj src/YasminLetiereConfeitaria.Domain/
COPY src/YasminLetiereConfeitaria.Infrastructure/YasminLetiereConfeitaria.Infrastructure.csproj src/YasminLetiereConfeitaria.Infrastructure/
COPY src/YasminLetiereConfeitaria.Web/YasminLetiereConfeitaria.Web.csproj src/YasminLetiereConfeitaria.Web/

RUN dotnet restore

# Copiar todo o resto e realizar o publish em modo Release
COPY . .
RUN dotnet publish src/YasminLetiereConfeitaria.Web/YasminLetiereConfeitaria.Web.csproj -c Release -o /app/publish

# Estágio de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# O ASP.NET Core 8+ escuta na porta 8080 por padrão em contêineres não-root
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "YasminLetiereConfeitaria.Web.dll"]

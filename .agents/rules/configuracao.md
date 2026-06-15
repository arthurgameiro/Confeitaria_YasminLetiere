---
trigger: always_on
glob: "**/*"
description: "Regras técnicas obrigatórias e contexto do projeto Yasmin Letiere Confeitaria (.NET 10 MVC + PostgreSQL + JWT). Aplicar em TODAS as interações sem exceção."
---

# Projeto: Yasmin Letiere Confeitaria

## Contexto e Arquitetura

Sistema web para uma confeiteira artesanal brasileira. Stack: **.NET 10 MVC + EF Core + PostgreSQL + JWT via Cookie HttpOnly**.

### Estrutura da Solution (`YasminLetiereConfeitaria.slnx`)
```
src/
├── YasminLetiereConfeitaria.Domain/           # Entidades e interfaces (sem dependências externas)
├── YasminLetiereConfeitaria.Infrastructure/   # EF Core, repositórios, migrações, JWT
└── YasminLetiereConfeitaria.Web/              # Controllers, Views Razor, wwwroot
    └── (Controllers ficam em Presentation/ dentro do projeto Web)
```

### Entidades do Domínio
- `Product` — Nome, Descrição, Preço, ImageUrl, CategoryId, SeasonalTag (nullable), IsAvailable
- `Category` — Categorias fixas: Bolos Festivos, Doces Finos, Salgados
- `Sazonalidade` — Nome, DataInicio, DataFim, MensagemExpirada (Páscoa / Natal / Namorados)
- `User` — Apenas a confeiteira Yasmin (acesso ao painel admin)

### Constantes críticas do negócio
- **WhatsApp:** `https://wa.me/message/3DAIKNWMQVTAM1` — usar exatamente esse link em todas as views
- **Admin route:** `/Admin` protegida por `[Authorize]` + cookie JWT
- **Cardápio sazonal:** rota `/Home/Sazonal?season=Páscoa|Natal|Namorados`

---

## Banco de Dados PostgreSQL

### Credenciais locais padrão
- Usuário: `postgres` | Senha: `123456` | Porta: `5432`
- Manter em `appsettings.Development.json` e `docker-compose.yml`.

### ⛔ PROIBIDO: Jamais apague ou recrie a base de dados
Nunca execute os comandos abaixo. Esta é a regra mais crítica do projeto:
```
DROP DATABASE ...
dotnet ef database drop
docker-compose down -v
```

Toda alteração de schema deve ser feita via migração incremental:
```bash
dotnet ef migrations add NomeDaMigracao \
  --project src/YasminLetiereConfeitaria.Infrastructure \
  --startup-project src/YasminLetiereConfeitaria.Web

dotnet ef database update \
  --project src/YasminLetiereConfeitaria.Infrastructure \
  --startup-project src/YasminLetiereConfeitaria.Web
```

---

## Consultas LINQ com EF Core (Npgsql)

O Npgsql não traduz `string.Equals` com `StringComparison` para SQL — lança `InvalidOperationException`.

```csharp
// ⛔ ERRADO — nunca use em expressões EF Core:
.Where(x => string.Equals(x.Nome, valor, StringComparison.OrdinalIgnoreCase))
.Where(x => x.Nome.ToLower() == valor.ToLower())

// ✅ CORRETO — usa SQL nativo do PostgreSQL:
.Where(x => EF.Functions.ILike(x.Nome, valor))
```

---

## Datas e Fuso Horário

O Npgsql exige `DateTimeKind.Utc` para campos `timestamptz`.

```csharp
// ✅ Datas geradas no servidor:
var agora = DateTime.UtcNow;

// ✅ Datas de formulários (chegam como Unspecified — converter antes de salvar):
var dataUtc = DateTime.SpecifyKind(dataDoFormulario, DateTimeKind.Utc);
```

---

## Campos Decimais em Formulários HTML

O HTML5 exige ponto `.` como separador decimal no atributo `value`, independente do locale.

```razor
// ✅ CORRETO:
<input type="number" name="preco" step="0.01"
       value="@Model.Preco.ToString("F2", new System.Globalization.CultureInfo("en-US"))" />
```

---

## Qualidade de Código

```csharp
// ⛔ PROIBIDO no código da aplicação (só permitido em migrações geradas pelo EF):
#pragma warning disable CA1862
```

Resolva sempre por refatoração: use `?.`, `EF.Functions.ILike`, encapsule chamadas assíncronas.

---

## Segurança e JWT

O token JWT deve trafegar exclusivamente via cookie HttpOnly:
```csharp
options.Cookie.HttpOnly = true;
options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
```

---

## Imagens e Assets

- Pasta: `src/YasminLetiereConfeitaria.Web/wwwroot/images/`
- Nomes: minúsculas + underline. Ex: `bolo_brigadeiro_duo.jpg`, `yasmin_perfil_show.jpg`
- ⛔ Nunca use: `Foto 1.jpg`, `img_final_v3.png`

---

## Nomenclatura

Todas as novas entidades, tabelas, propriedades e rotas devem ser nomeadas em **português**:
- ✅ `Sazonalidade`, `DataInicio`, `DataFim`, `MensagemExpirada`
- ⛔ `Season`, `StartDate`, `EndDate`, `ExpiredMessage`

---

## Otimização de Builds

Execute `dotnet build` apenas:
1. Ao final de um conjunto de alterações para validar a solução completa.
2. Para depurar erros de sintaxe não identificáveis pela leitura do código.

⛔ Nunca execute builds intermediários para verificar alterações individuais.

---

## Validação e Agentes de Navegador

⛔ PROIBIDO: Nunca execute testes ou interações usando o agente de navegador (`browser_subagent`). Toda validação visual e de fluxo deve ser feita de forma manual ou por meios alternativos sem o uso de subagentes de navegação.

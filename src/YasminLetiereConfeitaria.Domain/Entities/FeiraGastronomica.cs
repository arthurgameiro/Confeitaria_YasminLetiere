using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    public class FeiraGastronomica
    {
        public Guid Id { get; }
        public string Nome { get; private set; } = null!;
        public string Local { get; private set; } = null!;
        public DateTime DataHora { get; private set; }
        public string? Descricao { get; private set; }
        public bool Ativo { get; private set; } = true;
        public DateTime CriadoEm { get; }
        public DateTime AtualizadoEm { get; private set; }

        private FeiraGastronomica() { } // EF Core

        public FeiraGastronomica(string nome, string local, DateTime dataHora, string? descricao = null)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Local = local;
            DataHora = DateTime.SpecifyKind(dataHora, DateTimeKind.Utc);
            Descricao = descricao;
            Ativo = true;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Atualizar(string nome, string local, DateTime dataHora, string? descricao)
        {
            Nome = nome;
            Local = local;
            DataHora = DateTime.SpecifyKind(dataHora, DateTimeKind.Utc);
            Descricao = descricao;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void SetAtivo(bool ativo)
        {
            Ativo = ativo;
            AtualizadoEm = DateTime.UtcNow;
        }
    }
}

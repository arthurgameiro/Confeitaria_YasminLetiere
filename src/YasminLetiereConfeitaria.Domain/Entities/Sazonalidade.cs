using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    public class Sazonalidade
    {
        public Guid Id { get; }
        public string Nome { get; } = null!;
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public string MensagemExpirada { get; private set; } = null!;
        public string? Icone { get; private set; }
        public bool Ativo { get; private set; } = true;
        public DateTime CriadoEm { get; }
        public DateTime AtualizadoEm { get; private set; }

        private Sazonalidade() { } // EF Core

        public Sazonalidade(string nome, DateTime dataInicio, DateTime dataFim, string mensagemExpirada, string? icone = null)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc);
            DataFim = DateTime.SpecifyKind(dataFim, DateTimeKind.Utc);
            MensagemExpirada = mensagemExpirada;
            Icone = icone;
            Ativo = true;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void AtualizarPeriodo(DateTime dataInicio, DateTime dataFim, string mensagemExpirada, string? icone)
        {
            DataInicio = DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc);
            DataFim = DateTime.SpecifyKind(dataFim, DateTimeKind.Utc);
            MensagemExpirada = mensagemExpirada;
            Icone = icone;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void SetAtivo(bool ativo)
        {
            Ativo = ativo;
            AtualizadoEm = DateTime.UtcNow;
        }
    }
}

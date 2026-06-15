using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    /// <summary>
    /// Depoimento / avaliação de cliente.
    /// Pode ser cadastrado manualmente (Google, WhatsApp, etc.) ou recebido diretamente.
    /// </summary>
    public class Depoimento
    {
        public Guid Id { get; private set; }
        public string NomeCliente { get; private set; } = null!;
        public string Texto { get; private set; } = null!;
        public int Nota { get; private set; } = 5;           // 1–5 estrelas
        public string? Fonte { get; private set; }           // "Google", "WhatsApp", etc.
        public bool Ativo { get; private set; } = true;
        public int Ordem { get; private set; } = 0;
        public DateTime DataDepoimento { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }

        private Depoimento() { } // EF Core

        public Depoimento(string nomeCliente, string texto, int nota, string? fonte, DateTime dataDepoimento, int ordem = 0)
        {
            Id = Guid.NewGuid();
            NomeCliente = nomeCliente;
            Texto = texto;
            Nota = Math.Clamp(nota, 1, 5);
            Fonte = fonte;
            DataDepoimento = DateTime.SpecifyKind(dataDepoimento, DateTimeKind.Utc);
            Ativo = true;
            Ordem = ordem;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Atualizar(string nomeCliente, string texto, int nota, string? fonte, DateTime dataDepoimento, int ordem)
        {
            NomeCliente = nomeCliente;
            Texto = texto;
            Nota = Math.Clamp(nota, 1, 5);
            Fonte = fonte;
            DataDepoimento = DateTime.SpecifyKind(dataDepoimento, DateTimeKind.Utc);
            Ordem = ordem;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void SetAtivo(bool ativo)
        {
            Ativo = ativo;
            AtualizadoEm = DateTime.UtcNow;
        }
    }
}

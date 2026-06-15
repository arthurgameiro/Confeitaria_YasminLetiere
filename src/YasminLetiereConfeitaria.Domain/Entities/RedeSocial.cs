using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    public class RedeSocial
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = null!;
        public string Url { get; private set; } = null!;
        public string Icone { get; private set; } = null!;  // Material Icon name
        public bool Ativo { get; private set; } = true;
        public int Ordem { get; private set; } = 0;
        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }

        private RedeSocial() { } // EF Core

        public RedeSocial(string nome, string url, string icone, int ordem = 0)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Url = url;
            Icone = icone;
            Ativo = true;
            Ordem = ordem;
            CriadoEm = DateTime.UtcNow;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void Atualizar(string nome, string url, string icone, int ordem)
        {
            Nome = nome;
            Url = url;
            Icone = icone;
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

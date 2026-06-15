using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    /// <summary>
    /// Configuração simples de chave-valor para parâmetros do sistema.
    /// Chave é a PK (string) — ex: "catalogo_sazonal_ativo".
    /// </summary>
    public class ConfiguracaoSistema
    {
        public string Chave { get; private set; } = null!;
        public string Valor { get; private set; } = null!;
        public DateTime AtualizadoEm { get; private set; }

        private ConfiguracaoSistema() { } // EF Core

        public ConfiguracaoSistema(string chave, string valor)
        {
            Chave = chave;
            Valor = valor;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void SetValor(string valor)
        {
            Valor = valor;
            AtualizadoEm = DateTime.UtcNow;
        }
    }
}

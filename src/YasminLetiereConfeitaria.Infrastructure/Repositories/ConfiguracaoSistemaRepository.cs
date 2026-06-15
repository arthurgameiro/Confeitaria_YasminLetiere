using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class ConfiguracaoSistemaRepository(ApplicationDbContext context)
        : IConfiguracaoSistemaRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ConfiguracaoSistema?> GetAsync(string chave)
        {
            return await _context.ConfiguracoesSistema.FindAsync(chave);
        }

        public async Task SetAsync(string chave, string valor)
        {
            var config = await _context.ConfiguracoesSistema.FindAsync(chave);
            if (config == null)
            {
                config = new ConfiguracaoSistema(chave, valor);
                _context.ConfiguracoesSistema.Add(config);
            }
            else
            {
                config.SetValor(valor);
                _context.Entry(config).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<bool> GetBoolAsync(string chave, bool valorPadrao = true)
        {
            var config = await GetAsync(chave);
            if (config == null) return valorPadrao;
            return config.Valor == "true";
        }
    }
}

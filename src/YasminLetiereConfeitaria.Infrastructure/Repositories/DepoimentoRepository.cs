using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class DepoimentoRepository(ApplicationDbContext context)
        : BaseRepository<Depoimento>(context), IDepoimentoRepository
    {
        public async Task<IEnumerable<Depoimento>> GetAtivosOrdenadasAsync()
        {
            return await _context.Depoimentos
                .Where(d => d.Ativo)
                .OrderBy(d => d.Ordem)
                .ThenByDescending(d => d.DataDepoimento)
                .ToListAsync();
        }
    }
}

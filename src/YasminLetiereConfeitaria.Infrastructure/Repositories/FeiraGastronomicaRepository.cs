using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class FeiraGastronomicaRepository(ApplicationDbContext context) : BaseRepository<FeiraGastronomica>(context), IFeiraGastronomicaRepository
    {
        public async Task<IEnumerable<FeiraGastronomica>> GetProximasAtivasAsync()
        {
            var agora = DateTime.UtcNow;
            var result = await _dbSet
                .Where(f => f.Ativo && f.DataHora >= agora)
                .OrderBy(f => f.DataHora)
                .ToListAsync();
            return result;
        }
    }
}

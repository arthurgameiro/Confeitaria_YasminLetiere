using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class RedeSocialRepository(ApplicationDbContext context)
        : BaseRepository<RedeSocial>(context), IRedeSocialRepository
    {
        public async Task<IEnumerable<RedeSocial>> GetAtivasOrdenadasAsync()
        {
            return await _context.RedesSociais
                .Where(r => r.Ativo)
                .OrderBy(r => r.Ordem)
                .ThenBy(r => r.Nome)
                .ToListAsync();
        }
    }
}

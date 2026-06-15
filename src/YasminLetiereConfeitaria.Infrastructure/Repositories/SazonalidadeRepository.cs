using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class SazonalidadeRepository(ApplicationDbContext context) : BaseRepository<Sazonalidade>(context), ISazonalidadeRepository
    {
        public async Task<Sazonalidade?> GetByNomeAsync(string nome)
        {
            var result = await _dbSet
                .FirstOrDefaultAsync(s => EF.Functions.ILike(s.Nome, nome));
            return result;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category>(context), ICategoryRepository
    {
        public async Task<IEnumerable<Category>> GetOrderedAsync()
        {
            var result = await _dbSet
                .OrderBy(c => c.Order)
                .ToListAsync();
            return result;
        }
    }
}

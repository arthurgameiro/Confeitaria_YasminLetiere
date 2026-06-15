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
    public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAvailableAsync()
        {
            var result = await _dbSet
                .Include(p => p.Category)
                .Where(p => p.IsAvailable)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetBySeasonalTagAsync(string seasonalTag)
        {
            var result = await _dbSet
                .Include(p => p.Category)
                .Where(p => p.SeasonalTag != null && EF.Functions.ILike(p.SeasonalTag, seasonalTag))
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetFixedMenuAsync()
        {
            var result = await _dbSet
                .Include(p => p.Category)
                .Where(p => string.IsNullOrEmpty(p.SeasonalTag))
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
        {
            var result = await _dbSet
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
            return result;
        }
    }
}

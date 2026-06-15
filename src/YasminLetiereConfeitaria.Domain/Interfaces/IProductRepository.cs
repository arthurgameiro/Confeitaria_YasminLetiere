using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAvailableAsync();
        Task<IEnumerable<Product>> GetBySeasonalTagAsync(string seasonalTag);
        Task<IEnumerable<Product>> GetFixedMenuAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId);
    }
}

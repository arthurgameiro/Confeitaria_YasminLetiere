using System.Collections.Generic;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetOrderedAsync();
    }
}

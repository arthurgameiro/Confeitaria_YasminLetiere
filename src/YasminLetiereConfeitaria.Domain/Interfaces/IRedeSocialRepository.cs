using System.Collections.Generic;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Domain.Interfaces
{
    public interface IRedeSocialRepository : IRepository<RedeSocial>
    {
        Task<IEnumerable<RedeSocial>> GetAtivasOrdenadasAsync();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Domain.Interfaces
{
    public interface IDepoimentoRepository : IRepository<Depoimento>
    {
        Task<IEnumerable<Depoimento>> GetAtivosOrdenadasAsync();
    }
}

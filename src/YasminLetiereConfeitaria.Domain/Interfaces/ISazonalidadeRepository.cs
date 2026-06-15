using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Domain.Interfaces
{
    public interface ISazonalidadeRepository : IRepository<Sazonalidade>
    {
        Task<Sazonalidade?> GetByNomeAsync(string nome);
    }
}

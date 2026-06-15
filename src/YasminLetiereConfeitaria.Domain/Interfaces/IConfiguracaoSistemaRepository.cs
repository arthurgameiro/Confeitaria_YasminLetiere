using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Domain.Interfaces
{
    public interface IConfiguracaoSistemaRepository
    {
        Task<ConfiguracaoSistema?> GetAsync(string chave);
        Task SetAsync(string chave, string valor);
        Task<bool> GetBoolAsync(string chave, bool valorPadrao = true);
    }
}

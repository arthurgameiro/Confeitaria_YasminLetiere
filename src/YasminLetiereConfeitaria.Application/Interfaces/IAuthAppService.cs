using System.Threading.Tasks;

namespace YasminLetiereConfeitaria.Application.Interfaces
{
    public interface IAuthAppService
    {
        Task<string?> LoginAsync(string username, string password);
    }
}

using YasminLetiereConfeitaria.Domain.Entities;

namespace YasminLetiereConfeitaria.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}

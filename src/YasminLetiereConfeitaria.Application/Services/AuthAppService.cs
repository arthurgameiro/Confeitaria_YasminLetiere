using System.Threading.Tasks;
using YasminLetiereConfeitaria.Application.Interfaces;
using YasminLetiereConfeitaria.Domain.Interfaces;

namespace YasminLetiereConfeitaria.Application.Services
{
    public class AuthAppService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService) : IAuthAppService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<string?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return null;
            }

            var isPasswordValid = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return null;
            }

            return _jwtService.GenerateToken(user);
        }
    }
}

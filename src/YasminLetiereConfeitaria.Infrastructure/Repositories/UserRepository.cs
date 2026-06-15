using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YasminLetiereConfeitaria.Domain.Entities;
using YasminLetiereConfeitaria.Domain.Interfaces;
using YasminLetiereConfeitaria.Infrastructure.Context;

namespace YasminLetiereConfeitaria.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _context.Users
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Username, username));
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}

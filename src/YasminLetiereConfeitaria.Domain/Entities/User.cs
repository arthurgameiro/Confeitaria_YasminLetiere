using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    public class User
    {
        public Guid Id { get; }
        public string Username { get; } = null!;
        public string PasswordHash { get; } = null!;
        public string Role { get; } = null!;
        public DateTime CreatedAt { get; }

        private User() { } // EF Core

        public User(string username, string passwordHash, string role = "Admin")
        {
            Id = Guid.NewGuid();
            Username = username.ToLowerInvariant();
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

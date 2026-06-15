using System;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; } = null!;
        public bool IsAvailable { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; } = null!;
        public string? SeasonalTag { get; private set; } // null para fixo, "Páscoa", "Natal", etc.
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        private Product() { } // EF Core

        public Product(string name, string description, decimal price, string imageUrl, Guid categoryId, string? seasonalTag = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Update(name, description, price, imageUrl, categoryId, seasonalTag);
            SetAvailability(true);
        }

        public void Update(string name, string description, decimal price, string imageUrl, Guid categoryId, string? seasonalTag = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            SeasonalTag = seasonalTag;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

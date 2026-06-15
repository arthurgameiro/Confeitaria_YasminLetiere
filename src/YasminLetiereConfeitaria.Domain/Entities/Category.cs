using System;
using System.Collections.Generic;

namespace YasminLetiereConfeitaria.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; }
        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Icon { get; private set; } = null!;
        public int Order { get; private set; }
        public ICollection<Product> Products { get; } = [];

        private Category() { } // EF Core

        public Category(string name, string description, string icon, int order)
        {
            Id = Guid.NewGuid();
            Update(name, description, icon, order);
        }

        public void Update(string name, string description, string icon, int order)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.");

            Name = name;
            Description = description;
            Icon = icon;
            Order = order;
        }
    }
}

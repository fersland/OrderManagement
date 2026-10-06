using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        public Product() { }

        public Product(string description, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("La descripcion es requerida.");
            if (decimal.IsNegative(price)) throw new ArgumentException("El precio es requerido");

            Description = description;
            Price = price;
            Stock = stock;
        }
    }
}

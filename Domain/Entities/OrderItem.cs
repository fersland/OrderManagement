using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        private OrderItem() { }

        public OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("La cantidad debe ser mayor a cero.");
            if (unitPrice <= 0) throw new ArgumentException("El precio debe ser mayor a cero.");

            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}

using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public string CustomerEmail { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }


        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> items => _items.AsReadOnly();

        private Order() { }

        public Order(string customerName, string customerEmail)
        {
            Id = Guid.NewGuid();
            CustomerName = !string.IsNullOrEmpty(customerName) ? customerName : throw new ArgumentException("El nombre es requerido.");
            CustomerEmail = !string.IsNullOrEmpty(customerEmail) ? customerEmail : throw new ArgumentException("El correo es requerido.");
            CreatedAt = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        } 

        public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("No se puede modificar items de una orden procesada.");
            var item = new OrderItem(productId, productName, unitPrice, quantity);
            _items.Add(item);
            CalculateTotal();
        }

        public void MarkPaid()
        {
            if(Status != OrderStatus.Pending)
                throw new InvalidOperationException("Solo se pueden pagar ordenenes pedientes");

            if (!_items.Any())
                throw new InvalidOperationException("No se puede pagar una orden vacia.");

            Status = OrderStatus.Paid;
        }

        private void CalculateTotal()
        {
            TotalAmount = _items.Sum(x => x.UnitPrice * x.Quantity);
        }
    }
}

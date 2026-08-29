using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Ci { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }

        private Customer() { }

        public Customer(Guid customerId, string ci, string fname, string lname, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(ci)) throw new ArgumentException("La cedula es requerida.");
            if (string.IsNullOrWhiteSpace(fname)) throw new ArgumentException("El nombre del cliente es requerido.");

            Id = Guid.NewGuid();
            Ci = ci;
            FirstName = fname;
            LastName = lname;
            Phone = phone ?? string.Empty;
            Email = email ?? string.Empty;
        }
    }
}

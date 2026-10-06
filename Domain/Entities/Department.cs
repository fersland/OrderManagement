using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }

        private Department() { }

        public Department(Guid id, string name, string code)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El campo Nombre es requerido.");

            Id = Guid.NewGuid();
            Name = name;
            Code = code ?? string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Developer() { }

        public Developer(string name, int age)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es requerido.");
            if (age <= 0) throw new ArgumentException("La edad es requerida.");

            Name = name;
            Age = age;
        }

        public void UpdateDetails(string name, int age)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("El nombre es requerido");
            if (age <= 0) throw new ArgumentException("La edad es requerida.");

            this.Name = name;
            this.Age = age;
        }
    }
}

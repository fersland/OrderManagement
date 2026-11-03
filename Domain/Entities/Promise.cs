using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Promise
    {
        public int Id {get; set;}
        public string Name { get; set; }
        public int Age { get; set; }
        public string Colour { get; set; }

        public Promise() { }

        public Promise(string name, int age, string colour)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("El campo nombre es requerido.");
            Name = name;
            Age = age;
            Colour = colour;
        }

        public void UpdateDetails(string name, int age, string colour)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("El campo nombre es requerido.");
            Name = name;
            Age = age;
            Colour = colour;
        }

    }
}

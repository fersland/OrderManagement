using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pet
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Colour { get; private set; }
        public int Age { get; private set; }
        
        public Pet() { }

        public Pet(string name, string colour, int age)
        {
            if (name == null) throw new ArgumentException("El nombre es requerido.");
            if (age == 0) throw new ArgumentException("La edad es requerida.");

            Name = name;
            Colour = colour ?? string.Empty;
            Age = age;
        }

        public void PetUpdate(string name, string colour, int age)
        {
            if (name == null) throw new ArgumentException("El nombre es requerido.");
            if (age == 0) throw new ArgumentException("La edad es requerida.");

            Name = name;
            Colour = colour ?? string.Empty;
            Age = age;
        }

    }
}

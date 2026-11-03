using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Player
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }

        public Player() { }
        public Player(string name, int age)
        {
            if (string.IsNullOrEmpty(Name)) throw new ArgumentException("El nombre es requerido");
            if (age <= 16) throw new ArgumentException("La edad debe ser mayor o igual a 16 años");

            Name = name;
            Age = age;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Films
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int ReleaseYear { get; private set; }
        

        public Films() { }

        public Films(string title, string description, int releaseYear)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("El titulo es requerido.");
            if( releaseYear < 1888) throw new ArgumentException("El año de lanzamiento es invalido.");

            Title = title;
            Description = description ?? string.Empty;
            ReleaseYear = releaseYear;

        }

        public void UpdateDetails(string Title, string Description, int ReleaseYear)
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("El titulo es requerido.");
            if (ReleaseYear < 1888) throw new ArgumentException("El año de lanzamiento es invalido.");
            this.Title = Title;
            this.Description = Description ?? string.Empty;
            this.ReleaseYear = ReleaseYear;
        }

        public void UpdateFilms(string Title, string Description, int ReleaseYear)
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("El titulo es requerido.");
            if (ReleaseYear <= 1950) throw new ArgumentException("El año de lanzamiento es invalido.");

            this.Title = Title;
            this.Description = Description ?? string.Empty;
            this.ReleaseYear = ReleaseYear;
        }

        public void UpdateFilmYa(string Title, string Description, int ReleaseYear)
        {
            if (ReleaseYear <= 1900) throw new ArgumentException("El año no puede menor a 1900");
            if (!string.IsNullOrWhiteSpace(Title)) throw new ArgumentException("El titulo es requerido.");

            this.Title = Title;
            this.Description = Description ?? string.Empty;
            this.ReleaseYear = ReleaseYear;
        }
    }
}

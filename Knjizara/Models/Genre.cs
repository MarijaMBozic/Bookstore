using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Genre
    {
        public static int brojac;
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimalan broj slova 3 maximalan 50!")]
        [RegularExpression("[A-Za-z0-9 -]+")]
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>() { };
        public bool Deleted { get; set; }

        public Genre()
        {
           brojac = Id++;
        }
        public Genre(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Genre(int id, string name, bool deleted)
        {
            this.Id = id;
            this.Name = name;
            this.Deleted = deleted;
        }

        public Genre(int id, string name, bool deleted, List<Book> books)
        {
            this.Id = id;
            this.Name = name;
            this.Deleted = deleted;
            this.Books = books;
        }

    }
}
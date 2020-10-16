using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    
    public class Book
    {
        public static int brojac = 0;
        public int Id { get; set; }
        [Required(ErrorMessage ="Obavezno polje!")]
        [StringLength(50, MinimumLength =3,ErrorMessage ="Minimalan broj slova 3 maximalan 50!")]
        [RegularExpression("[A-Za-z0-9 -]+")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        public double Price { get; set; }

        public Genre Genre { get; set; }
        public bool Deleted { get; set; }

        public Book()
        {
            Id = brojac++;
        }

        public Book(int id,string name, double price, bool deleted)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Deleted = deleted;
        }
        public Book(string name, double price,bool deleted)
        {
            this.Name = name;
            this.Price = price;
            this.Deleted = deleted;
        }
        public Book(int id, string name, double price, Genre genre)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Genre = genre;
        }
        public Book(int id, string name, double price, Genre genre, bool deleted)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Genre = genre;
            this.Deleted = deleted;
        }
    }
}
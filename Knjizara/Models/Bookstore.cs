using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Knjizara.Models
{
    public class Bookstore
    {
        public static int BrojacId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Book { get; set; }

        public Bookstore(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            Book = new List<Book>();
        }

    }
}
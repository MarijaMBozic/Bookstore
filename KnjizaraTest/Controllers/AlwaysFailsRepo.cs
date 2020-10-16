using Knjizara.Models;
using Knjizara.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnjizaraTest.Controllers
{
    public class AlwaysFailsRepo : IBookRepository
    {
        Dictionary<int, Book> books = new Dictionary<int, Book>();
        public AlwaysFailsRepo()
        {
            var b1 = new Book(1, "Knjiga1", 120, false);
            var b2 = new Book(2, "Knjiga2", 20, false);
            var b3 = new Book(3, "Knjiga3", 1200, false);
            var b4 = new Book(4, "Knjiga4", 200, false);

            books.Add(1, b1);
            books.Add(2, b2);
            books.Add(3, b3);
            books.Add(4, b4);
        }

        public int Create(Book book)
        {        
            books.Add(book.Id, book);

            return book.Id;
        }

        public bool Delete(int id)
        {
            if (books.ContainsKey(id))
            {
                books.Remove(id);
                return true;
            }
            else
            {
                throw new Exception();
            }
        }

        public IEnumerable<Book> GetAll()
        {
            return books.Values;
        }

        public Book GetById(int id)
        {
            if (books.ContainsKey(id))
            {
                return books[id];
            }

            return null;
        }

        public bool Update(Book book)
        {
            if (books.ContainsKey(book.Id))
            {
                books[book.Id] = book;

                return true;
            }

            return false;
        }
    }
}

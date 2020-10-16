using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Repository.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        int Create(Book book);
        bool Update(Book book);
        bool Delete(int id);
    }
}

using Knjizara.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knjizara.Repository.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre GetById(int id);
        int Create(Genre genre);
        bool Update(Genre genre);
        bool Delete(int id);


    }
}

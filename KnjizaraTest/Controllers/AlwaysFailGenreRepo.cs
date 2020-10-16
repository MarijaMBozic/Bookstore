using Knjizara.Models;
using Knjizara.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnjizaraTest.Controllers
{
    public class AlwaysFailGenreRepo: IGenreRepository
    {
        Dictionary<int, Genre> genres = new Dictionary<int, Genre>();

        public AlwaysFailGenreRepo()
        {
            var g1 = new Genre(1, "Comedy");
            var g2 = new Genre(1, "Horror");
            var g3 = new Genre(1, "Science");

            genres.Add(1, g1);
            genres.Add(2, g2);
            genres.Add(3, g3);

        }

        public int Create(Genre genre)
        {
            genres.Add(genre.Id, genre);

            return genre.Id;
        }

        public bool Delete(int id)
        {
            if(genres.ContainsKey(id))
            {
                genres.Remove(id);
                return true;
            }
            return false;
        }

        public IEnumerable<Genre> GetAll()
        {
            return genres.Values;
        }

        public Genre GetById(int id)
        {
           if(genres.ContainsKey(id))
            {
                return genres[id];
            }
            return null;
        }

        public bool Update(Genre genre)
        {
            if(genres.ContainsKey(genre.Id))
            {
                genres[genre.Id] = genre;
                return true;
            }

            return false;
        }
    }
}

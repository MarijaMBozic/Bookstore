using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knjizara.Models;
using Knjizara.Repository.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnjizaraTest.Controllers
{
    public class TestGenreRepo : IGenreRepository
    {
        Dictionary<int, Genre> genres = new Dictionary<int, Genre>();

        public TestGenreRepo()
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
            var id = genres.Count + 1;
            genre.Id = id;
            genres.Add(id, genre);
            return id;
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
            if (genres.ContainsKey(id))
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

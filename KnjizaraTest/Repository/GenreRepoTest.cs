using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Knjizara.Models;
using Knjizara.Repository;
using NUnit.Framework.Internal;

namespace KnjizaraTest.Repository
{

    [TestClass]
    public class GenreRepoTest
    { 

        [TestMethod]
        public void GenreUpdateTest()
        {
            var genre = new Genre();
            genre.Name = "Zanr4";

            var genRepo = new GenreRepo();
            var id = genRepo.Create(genre);

            Assert.IsTrue(id != -1);

            genre.Id = id;
            genre.Name = "Change name";

            var update = genRepo.Update(genre);

            Assert.IsTrue(update);

            var deleted = genRepo.DeleteActual(id);

            Assert.IsTrue(deleted);
        }
        [TestMethod]
        public void GenreGetByIdTest()
        {
            var genre = new Genre();
            genre.Name = "Zanr1";

            var genRepo = new GenreRepo();
            var id = genRepo.Create(genre);

            var findeGenre = genRepo.GetById(id);
            Assert.IsNotNull(findeGenre);

            var deleted = genRepo.DeleteActual(id);

            Assert.IsTrue(deleted);
        }



    }
}

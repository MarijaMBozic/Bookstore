using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Knjizara.Controllers;
using Knjizara.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnjizaraTest.Controllers
{
    [TestClass]
    public class GenreControlerTest 
    {
        [TestMethod]
        public void TestCreate()
        {
            var genreController = new GenreController();
            genreController.genreRepo = new TestGenreRepo();

            var genre = new Genre();
            genre.Name = "Drama";


            var result = genreController.Create(genre) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var genreController = new GenreController();
            genreController.genreRepo = new TestGenreRepo();

            Genre genre = new Genre();
            genre.Id = 1;
            genre.Name = "Drama";

            //FormCollection col = new FormCollection();
            //NameValueCollection nameValCol = new NameValueCollection();
            //nameValCol.Add("Id", genre.Id.ToString());
            //nameValCol.Add("Name", genre.Name);

            //col.Add(nameValCol);

            var result = genreController.Update(genre) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDelete()
        {
            var genreController = new GenreController();
            genreController.genreRepo = new TestGenreRepo();

            Genre genre = new Genre();
            genre.Id = 2;

            var result = genreController.Delete(genre.Id, new FormCollection()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateAlwaysFail()
        {
            var genreController = new GenreController();
            genreController.genreRepo = new AlwaysFailGenreRepo();

            var genre = new Genre();
            genre.Id = 2;
            genre.Name = "NestoTest";

            var result = genreController.Create(genre) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUpdateAlwaysFail()
        {
            var genreController = new GenreController();
            genreController.genreRepo = new AlwaysFailGenreRepo();

            int genreId = genreController.genreRepo.GetAll().ToList().Max(g => g.Id) + 1;
            var result = genreController.Update(genreId) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeleteAlwaysFail()
        {
            var genreController = new GenreController();
            genreController.genreRepo = new AlwaysFailGenreRepo();

            int genreId = genreController.genreRepo.GetAll().ToList().Max(g => g.Id) + 1;
            var result = genreController.Delete(genreId) as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}

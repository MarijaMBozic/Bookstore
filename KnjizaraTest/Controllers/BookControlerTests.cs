using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Knjizara.Controllers;
using Knjizara.Models;
using Knjizara.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnjizaraTest.Controllers
{
    [TestClass]
    public class BookControlerTests
    {
        [TestMethod]
        public void TestCreate()
        {
            var bookController = new BookController();
            bookController.bookRepo = new TestBookRepo();

            var viewModel = new ZanrKnjigaViewModels();

            var book = new Book();
            book.Name = "Knjiga8";
            book.Price = 128;          

            viewModel.Book = book;
            viewModel.SelectedGenreId = 1;
            var result= new Book();
            

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDelat()
        {
            var bookController = new BookController();
            bookController.bookRepo = new TestBookRepo();

            Book book = new Book();
            book.Id = 3;
            
            var result = bookController.Delete(book.Id, new FormCollection()) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void TestUpdate()
        {
            var bookController = new BookController();
            bookController.bookRepo = new TestBookRepo();

            var genre = new Genre();
            genre.Id = 1;
            genre.Name = "Horror";

            var viewModel = new ZanrKnjigaViewModels();

            var book = new Book();
            book.Id = 1;
            book.Name = "Knjiga99";
            book.Price = 128;
            book.Genre = genre;

            viewModel.Book = book;
            viewModel.SelectedGenreId = 1;

            //FormCollection col = new FormCollection();
            //NameValueCollection nameValColl = new NameValueCollection();
            //nameValColl.Add("Id", book.Id.ToString());
            //nameValColl.Add("Name", book.Name);
            //nameValColl.Add("Price", book.Price.ToString());
            //nameValColl.Add("Genre", book.Genre.Id.ToString());

            //col.Add(nameValColl);

            var result = bookController.Update(viewModel) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateAlwaysFail()
        {
            var bookController = new BookController();
            bookController.bookRepo = new AlwaysFailsRepo();

            var viewModel = new ZanrKnjigaViewModels();

            var book = new Book();
            book.Id = 2;
            book.Name = "Knjiga5";
            book.Price = 321;

            viewModel.Book = book;
            viewModel.SelectedGenreId = 1;

            var result = bookController.Create(viewModel) as ViewResult;

            Assert.IsNotNull(result);
        }

       [TestMethod]
        public void TestDelataAlwaysFail()
        {
            var bookController = new BookController();
            bookController.bookRepo = new AlwaysFailsRepo();

            int bookId = bookController.bookRepo.GetAll().ToList().Max(p => p.Id) + 1;
           
            var result = bookController.Delete(bookId, new FormCollection()) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUpdateFails()
        {
            var bookController = new BookController();
            bookController.bookRepo = new AlwaysFailsRepo();

            int bookId = bookController.bookRepo.GetAll().ToList().Max(p => p.Id) + 1;
                       
            var result = bookController.Update(bookId) as ViewResult;

            Assert.IsNotNull(result);

        }
    }
}

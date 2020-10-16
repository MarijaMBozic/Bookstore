using System;
using Knjizara.Models;
using Knjizara.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnjizaraTest.Repository
{
    [TestClass]
    public class BookRepoTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var genre = new Genre();
            genre.Name = "Zanr";

            var genreRepo = new GenreRepo();
            var id = genreRepo.Create(genre);

            var book = new Book();
            book.Name = "Knjiga1";
            book.Price = 123;
            book.Genre = genre;

            var bookRepo = new BookRepo();
            var idBook = bookRepo.Create(book);

            Assert.IsTrue(idBook != -1);

            book.Id = idBook;
            book.Name = "Knjiga88996";
            book.Price = 321;
            book.Genre.Id = id;

            var update = bookRepo.Update(book);

            Assert.IsTrue(update);

            var delete = bookRepo.DeleteActual(idBook);

            Assert.IsTrue(delete);

            var delGenre = genreRepo.DeleteActual(id);
            Assert.IsTrue(delGenre);
        }

        [TestMethod]
        public void TestGetBtyId()
        {
            var gen = new Genre();
            gen.Name = "Zanr1";

            var genRepo = new GenreRepo();
            var id = genRepo.Create(gen);

            var book = new Book();
            book.Name = "KnjigaTest";
            book.Price = 123;
            book.Genre = gen;

            var bookRepo = new BookRepo();

            var newBookId = bookRepo.Create(book);

            Assert.IsNotNull(newBookId);

            var delGenre = genRepo.DeleteActual(id);
            Assert.IsTrue(delGenre);

            var delete = bookRepo.DeleteActual(newBookId);

            Assert.IsTrue(delete);
        }
    }
}

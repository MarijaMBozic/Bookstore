using Knjizara.Models;
using Knjizara.Repository;
using Knjizara.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class GenreController : Controller
    {
        public IGenreRepository genreRepo { get; set; } = new GenreRepo();
        public IBookRepository bookRepo { get; set; } = new BookRepo();


        // GET: Genre
        public ActionResult Index()
        {
            return View(genreRepo.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Genre());
        }

        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newGenre = genreRepo.Create(genre);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return View(genre);
        }

        public ActionResult Details(int id)
        {
            var genre = genreRepo.GetById(id);

            return View(genre);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var genre = genreRepo.GetById(id);

            return View(genre);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var booklist = bookRepo.GetAll();
                foreach (var book in booklist)
                {
                    if (book.Genre.Id == id)
                    {
                        bookRepo.Delete(book.Id);
                    }
                }

                genreRepo.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Message"] = "Doslo je do greske prilikom brisanja!";
                return View("Error");
            }
        }

        public ActionResult DeleteGenre()
        {
            return View(genreRepo.GetAll());

        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var genre = genreRepo.GetById(id);


            return View(genre);
        }
        [HttpPost]
        public ActionResult Update(Genre genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    genreRepo.Update(genre);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return View(genre);
        }
    }
}
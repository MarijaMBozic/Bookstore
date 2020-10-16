using Knjizara.Models;
using Knjizara.Repository;
using Knjizara.Repository.Interfaces;
using Knjizara.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Knjizara.Controllers
{
    public class BookController : Controller
    {
        public IBookRepository bookRepo = new BookRepo();
        public IGenreRepository genRepo = new GenreRepo();
        public BookRepo bRepo = new BookRepo();
        public ActionResult Index()
        {
            return View(bookRepo.GetAll());
        }

        public ActionResult Details(int id)
        {
            var book = bookRepo.GetById(id);

            return View(book);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var gb = new ZanrKnjigaViewModels();
            gb.Zanrovi = genRepo.GetAll().ToList();
            gb.Book = new Book();

            return View(gb);
        }

        [HttpPost]
        public ActionResult Create(ZanrKnjigaViewModels gb)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var book = gb.Book;

                    var proveraNaziv = bRepo.SearchByNAme(book.Name);
                    if (proveraNaziv.Name != null)
                    {
                        TempData["Message"] = "Knjiga sa navedenim nazivom vec postojii u bazi podataka!";
                        return View("Error");
                    }

                    var genre = genRepo.GetById(gb.SelectedGenreId);
                    book.Genre = genre;                                    

                    bookRepo.Create(book);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            gb.Zanrovi = genRepo.GetAll().ToList();
            return View(gb);

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = bookRepo.GetById(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bookRepo.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Message"] = "Doslo je do greske prilikom brisanja knjige!";
                return View("/Genre/Error");
            }
        }

        public ActionResult DeletedBook()
        {
            return View(bookRepo.GetAll());
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var book = new ZanrKnjigaViewModels();
            book.Book = bookRepo.GetById(id);
            book.Zanrovi = genRepo.GetAll().ToList();
            return View(book);
        }
        [HttpPost]
        public ActionResult Update(ZanrKnjigaViewModels zkwm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var book = zkwm.Book;
                    book.Genre= new Genre(){ Id = zkwm.SelectedGenreId };

                    bookRepo.Update(book);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            zkwm.Zanrovi = genRepo.GetAll().ToList();
            return View(zkwm);
        }

        [HttpGet]
        public ActionResult Search()
        {
            var gb = new ZanrKnjigaViewModels();
            gb.Zanrovi = genRepo.GetAll().ToList();

            return View(gb);
        }

        [HttpPost]
        public ActionResult Search(ZanrKnjigaViewModels gb)
        {
            var idGen = gb.SelectedGenreId;
            return View("SearchResault", bRepo.Search(idGen));
        }

        public ActionResult Sort(string opcija, int smer)
        {
            var lista = bookRepo.GetAll();
            var listaSortiraniKnjiga = new List<Book>();

            if (opcija == "Name" && smer == 1)
            {
                listaSortiraniKnjiga = lista.OrderBy(x => x.Name).ToList();
            }
            else if (opcija == "Name" && smer == -1)
            {
                listaSortiraniKnjiga = lista.OrderByDescending(x => x.Name).ToList();
            }
            else if (opcija == "Price" && smer == 1)
            {
                listaSortiraniKnjiga = lista.OrderBy(x => x.Price).ToList();
            }
            else
            {
                listaSortiraniKnjiga = lista.OrderByDescending(x => x.Price).ToList();
            }

            return View(listaSortiraniKnjiga);
        }
    }
}
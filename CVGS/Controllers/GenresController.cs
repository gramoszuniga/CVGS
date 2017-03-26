using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;
using CVGS.ViewModels;

namespace CVGS.Controllers
{
    public class GenresController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Genres
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            GenreList genreList = new GenreList();
            foreach (PersonGenre personGenre in db.PersonGenres.Where(pp => pp.personId == account.personId))
            {
                genreList.selectedGenres.Add(personGenre.genreCode);
            }
            ViewBag.genreCodes = new MultiSelectList(db.Genres, "genreCode", "genreCode", genreList.selectedGenres);
            return View(genreList);
        }

        // POST: Genres
        [HttpPost]
        public ActionResult Index([Bind(Include = "selectedGenres")] GenreList genreList)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (PersonGenre personGenre in db.PersonGenres.Where(pp => pp.personId == account.personId))
                    {
                        db.PersonGenres.Remove(personGenre);
                    }
                    db.SaveChanges();
                    foreach (string genreCode in genreList.selectedGenres)
                    {
                        PersonGenre personGenre = new PersonGenre();
                        personGenre.personId = account.personId;
                        personGenre.genreCode = genreCode;
                        db.PersonGenres.Add(personGenre);
                    }
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ViewBag.genreCodes = new MultiSelectList(db.Genres, "genreCode", "genreCode", genreList.selectedGenres);
            return RedirectToAction("", "Profile", new { userName = account.userName });
        }

        // GET: Genres/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "genreCode")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "genreCode")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;
using CVGS.Enumerations;

namespace CVGS.Controllers
{
    public class ReviewsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Reviews
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (account.roleCode != "employee")
            {
                throw new HttpException(403, "Access denied");
            }
            var reviews = db.Reviews.Include(r => r.Game).Include(r => r.Person).OrderByDescending(r => r.revDate).ThenBy(r => r.status);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Add(int? id)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            ViewBag.ratings = new SelectList(new int[] { 0, 1, 2, 3, 4, 5 });
            ViewBag.gameId = id;
            ViewBag.title = db.Games.Find(id).title;
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "gameId,rating,revComment")] Review review)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (db.Reviews.Where(r => r.personId == account.personId && r.gameId == review.gameId && r.status == ReviewEnums.ReviewStatus.Approved.ToString()).SingleOrDefault() != null)
            {
                TempData["infoMsg"] = "You have already reviewed this game.";
                return RedirectToAction("Details", "Reviews", new { id = review.gameId });
            }
            if (ModelState.IsValid)
            {
                try
                {
                    review.personId = account.personId;
                    review.status = ReviewEnums.ReviewStatus.Pending.ToString();
                    review.revDate = DateTime.Today;
                    db.Reviews.Add(review);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = review.reviewId });
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }

            ViewBag.ratings = new SelectList(new int[] { 0, 1, 2, 3, 4, 5 }, review.rating);
            ViewBag.gameId = review.gameId;
            return View(review);
        }

        // GET: Reviews/UpdateStatus/5
        public ActionResult UpdateStatus(int? id)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (account.roleCode != "employee")
            {
                throw new HttpException(403, "Access denied");
            }
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                throw new HttpException(404, "Not found");
            }
            ViewBag.statuses = new SelectList(Enum.GetValues(typeof(ReviewEnums.ReviewStatus)), review.status);
            return View(review);
        }

        // POST: Reviews/UpdateStatus/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus([Bind(Include = "reviewId,gameId,status")] Review review)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (account.roleCode != "employee")
            {
                throw new HttpException(403, "Access denied");
            }
            try
            {
                Review rvw = db.Reviews.Find(review.reviewId);
                rvw.status = review.status;
                db.Entry(rvw).State = EntityState.Modified;
                db.SaveChanges();
                List<Review> reviews = db.Reviews.Where(r => r.gameId == review.gameId && r.status == ReviewEnums.ReviewStatus.Approved.ToString()).ToList();
                decimal rateAVG = 0.00m;
                foreach (Review rev in reviews)
                {
                    rateAVG += rev.rating;
                }
                Game game = db.Games.Find(review.gameId);
                game.rateAVG = reviews.Count() > 0 ? rateAVG / reviews.Count() : 0.00m;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("");
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            ViewBag.statuses = new SelectList(Enum.GetValues(typeof(ReviewEnums.ReviewStatus)), review.status);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
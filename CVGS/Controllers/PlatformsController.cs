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
    public class PlatformsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Platforms
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            PlatformList platformList = new PlatformList();
            foreach (PersonPlatform personPlatform in db.PersonPlatforms.Where(pp => pp.personId == account.personId))
            {
                platformList.selectedPlatforms.Add(personPlatform.platformCode);
            }
            ViewBag.platformCodes = new MultiSelectList(db.Platforms, "platformCode", "platformCode", platformList.selectedPlatforms);
            return View(platformList);
        }

        // POST: Platforms
        [HttpPost]
        public ActionResult Index([Bind(Include = "selectedPlatforms")] PlatformList platformList)
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
                    foreach (PersonPlatform personPlatform in db.PersonPlatforms.Where(pp => pp.personId == account.personId))
                    {
                        db.PersonPlatforms.Remove(personPlatform);
                    }
                    db.SaveChanges();
                    foreach (string platformCode in platformList.selectedPlatforms)
                    {
                        PersonPlatform personPlatform = new PersonPlatform();
                        personPlatform.personId = account.personId;
                        personPlatform.platformCode = platformCode;
                        db.PersonPlatforms.Add(personPlatform);
                    }
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ViewBag.platformCodes = new MultiSelectList(db.Platforms, "platformCode", "platformCode", platformList.selectedPlatforms);
            return RedirectToAction("", "Profile", new { userName = account.userName });
        }

        // GET: Platforms/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(platform);
        }

        // GET: Platforms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Platforms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "platformCode")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                db.Platforms.Add(platform);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(platform);
        }

        // GET: Platforms/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(platform);
        }

        // POST: Platforms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "platformCode")] Platform platform)
        {
            if (ModelState.IsValid)
            {
                db.Entry(platform).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(platform);
        }

        // GET: Platforms/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Platform platform = db.Platforms.Find(id);
            if (platform == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(platform);
        }

        // POST: Platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Platform platform = db.Platforms.Find(id);
            db.Platforms.Remove(platform);
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

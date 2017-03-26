using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;

namespace CVGS.Controllers
{
    public class AccountsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Accounts
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Person).Include(a => a.Role);
            return View(accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.personId = new SelectList(db.People, "personId", "fName");
            ViewBag.roleCode = new SelectList(db.Roles, "roleCode", "roleCode");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "personId,userName,password,roleCode,loginAttempts,isLocked")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.personId = new SelectList(db.People, "personId", "fName", account.personId);
            ViewBag.roleCode = new SelectList(db.Roles, "roleCode", "roleCode", account.roleCode);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                throw new HttpException(404, "Not found");
            }
            ViewBag.personId = new SelectList(db.People, "personId", "fName", account.personId);
            ViewBag.roleCode = new SelectList(db.Roles, "roleCode", "roleCode", account.roleCode);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "personId,userName,password,roleCode,loginAttempts,isLocked")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.personId = new SelectList(db.People, "personId", "fName", account.personId);
            ViewBag.roleCode = new SelectList(db.Roles, "roleCode", "roleCode", account.roleCode);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
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
    public class EventsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Events
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;
            List<ViewModels.Event> events = new List<ViewModels.Event>();
            List<Event> vnts = account != null && account.roleCode == "employee" ? db.Events.ToList() : db.Events.Where(e => e.startDate >= DateTime.Now).ToList();
            foreach (var item in vnts.OrderBy(e => e.startDate))
            {
                ViewModels.Event evnt = new ViewModels.Event();
                evnt.eventId = item.eventId;
                evnt.title = item.title;
                evnt.startDate = item.startDate;
                evnt.endDate = item.endDate;
                evnt.regFee = item.regFee;
                if (account != null && db.PersonEvents.Where(pe => pe.eventId == item.eventId && pe.personId == account.personId).SingleOrDefault() != null)
                {
                    evnt.isJoined = true;
                }
                else
                {
                    evnt.isJoined = false;
                }
                events.Add(evnt);
            }
            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Event evnt = db.Events.Find(id);
            if (evnt == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(evnt);
        }

        // GET: Events/Create
        public ActionResult Create()
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
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "personId,title,startDate,endDate,regFee,venue,notes")] Event evnt)
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
            if (evnt.startDate < DateTime.Now)
            {
                ModelState.AddModelError("startDate", "Start date cannot be earlier than today.");
            }
            if (evnt.endDate < evnt.startDate)
            {
                ModelState.AddModelError("endDate", "End date cannot be earlier than start date.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    evnt.personId = account.personId;
                    db.Events.Add(evnt);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
                return RedirectToAction("Index");
            }
            ViewBag.personId = new SelectList(db.People, "personId", "fName", evnt.personId);
            return View(evnt);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
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
            Event evnt = db.Events.Find(id);
            if (evnt == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(evnt);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eventId,personId,title,startDate,endDate,regFee,venue,notes")] Event evnt)
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
            if (evnt.startDate < DateTime.Now)
            {
                ModelState.AddModelError("startDate", "Start date cannot be earlier than today.");
            }
            if (evnt.endDate < evnt.startDate)
            {
                ModelState.AddModelError("endDate", "End date cannot be earlier than start date.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(evnt).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ViewBag.personId = new SelectList(db.People, "personId", "fName", evnt.personId);
            return View(evnt);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
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
            Event evnt = db.Events.Find(id);
            if (evnt == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(evnt);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
                foreach (PersonEvent personEvent in db.PersonEvents.Where(e => e.eventId == id))
                {
                    db.PersonEvents.Remove(personEvent);
                }
                db.SaveChanges();
                Event evnt = db.Events.Find(id);
                db.Events.Remove(evnt);
                db.SaveChanges();
            }
            catch (Exception err)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("Index");
        }

        // GET: Events/Join/5
        public ActionResult Join(int? id)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            PersonEvent personEvent = db.PersonEvents.Where(pe => pe.eventId == id && pe.personId == account.personId).SingleOrDefault();
            if (personEvent != null)
            {
                TempData["infoMsg"] = "You have already joined this event.";
                return RedirectToAction("");
            }
            try
            {
                personEvent = new PersonEvent();
                personEvent.personId = account.personId;
                personEvent.eventId = (int)id;
                db.PersonEvents.Add(personEvent);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("");
        }

        // GET: Events/Unjoin/5
        public ActionResult Unjoin(int? id)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            PersonEvent personEvent = db.PersonEvents.Where(pe => pe.eventId == id && pe.personId == account.personId).SingleOrDefault();
            if (personEvent == null)
            {
                TempData["infoMsg"] = "You must have already joined this event.";
                return RedirectToAction("");
            }
            try
            {
                db.PersonEvents.Remove(personEvent);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("");
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
using CVGS.Enumerations;
using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CVGS.Controllers
{
    public class ProfileController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Profile/?userName=userName
        public ActionResult Index(string userName)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (userName == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Person person = db.People.Where(p => p.Account.userName == userName).SingleOrDefault();
            if (person == null)
            {
                throw new HttpException(404, "Not found");
            }
            FriendList friendList = db.FriendLists.Where(fl => fl.personId == account.personId && fl.friendId == person.personId).SingleOrDefault();
            ViewBag.isFriend = friendList != null;
            return View(person);
        }

        // GET: Profile/Edit
        public ActionResult Edit()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            Person person = db.People.Find(account.personId);
            if (person == null)
            {
                throw new HttpException(404, "Not found");
            }
            Profile profile = new Profile();
            profile.userName = person.Account.userName;
            profile.fName = person.fName;
            profile.mName = person.mName;
            profile.lName = person.lName;
            profile.dob = person.dob;
            profile.gender = person.gender;
            profile.phone = person.phone;
            profile.email = person.email;
            profile.acceptEmails = person.acceptEmails;
            CreditCard creditCard = person.CreditCards.Where(cc => cc.Person.personId == person.personId).SingleOrDefault();
            profile.number = creditCard.number;
            profile.name = creditCard.name;
            profile.expDate = creditCard.expDate;
            profile.creditCardType = creditCard.creditCardType;
            profile.CVV = creditCard.CVV;
            Address billAddress = person.PersonAddresses.Where(pa => pa.personId == person.personId && pa.type == "billing").SingleOrDefault().Address;
            profile.billStreet = billAddress.street;
            profile.billCity = billAddress.city;
            profile.billPostalCode = billAddress.postalCode;
            profile.billProvinceCode = billAddress.provinceCode;
            Address shipAddress = person.PersonAddresses.Where(pa => pa.personId == person.personId && pa.type == "shipping").SingleOrDefault().Address;
            profile.shipStreet = shipAddress.street;
            profile.shipCity = shipAddress.city;
            profile.shipPostalCode = shipAddress.postalCode;
            profile.shipProvinceCode = shipAddress.provinceCode;
            ViewBag.genders = new SelectList(Enum.GetValues(typeof(PersonEnums.Gender)), profile.gender);
            ViewBag.creditCardTypes = new SelectList(Enum.GetValues(typeof(CreditCardEnums.CreditCardType)), profile.creditCardType);
            ViewBag.shipProvinceCode = new SelectList(db.Provinces, "provinceCode", "provinceCode", profile.shipProvinceCode);
            ViewBag.billProvinceCode = new SelectList(db.Provinces, "provinceCode", "provinceCode", profile.billProvinceCode);
            return View(profile);
        }

        // POST: Profile/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fName,mName,lName,dob,gender,phone,email,acceptEmails,number,name,expDate,creditCardType,CVV,billStreet,billCity,billPostalCode,billProvinceCode,shipStreet,shipCity,shipPostalCode,shipProvinceCode")] Profile profile)
        {
            if (profile.dob > DateTime.Today)
            {
                ModelState.AddModelError("dob", "Birthdate cannot be later than today.");
            }
            if (profile.expDate <= DateTime.Today)
            {
                ModelState.AddModelError("expDate", "Expiry date cannot be earlier than the current month.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Person person = db.People.Find((Session["account"] as Account).personId);
                    person.fName = profile.fName;
                    person.mName = profile.mName;
                    person.lName = profile.lName;
                    person.dob = profile.dob;
                    person.gender = profile.gender;
                    person.phone = profile.phone;
                    person.email = profile.email;
                    person.acceptEmails = profile.acceptEmails;
                    db.Entry(person).State = EntityState.Modified;
                    CreditCard creditCard = db.CreditCards.Where(cc => cc.personId == person.personId).SingleOrDefault();
                    creditCard.number = profile.number;
                    creditCard.name = profile.name;
                    creditCard.expDate = profile.expDate;
                    creditCard.creditCardType = profile.creditCardType;
                    creditCard.CVV = profile.CVV;
                    db.Entry(creditCard).State = EntityState.Modified;
                    Address billAddress = person.PersonAddresses.Where(pa => pa.personId == person.personId && pa.type == "billing").SingleOrDefault().Address;
                    billAddress.street = profile.billStreet;
                    billAddress.city = profile.billCity;
                    billAddress.postalCode = profile.billPostalCode;
                    billAddress.provinceCode = profile.billProvinceCode;
                    db.Entry(billAddress).State = EntityState.Modified;
                    Address shipAddress = person.PersonAddresses.Where(pa => pa.personId == person.personId && pa.type == "shipping").SingleOrDefault().Address;
                    shipAddress.street = profile.shipStreet;
                    shipAddress.city = profile.shipCity;
                    shipAddress.postalCode = profile.shipPostalCode;
                    shipAddress.provinceCode = profile.shipProvinceCode;
                    db.Entry(shipAddress).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { userName = person.Account.userName });
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ViewBag.genders = new SelectList(Enum.GetValues(typeof(PersonEnums.Gender)), profile.gender);
            ViewBag.creditCardTypes = new SelectList(Enum.GetValues(typeof(CreditCardEnums.CreditCardType)), profile.creditCardType);
            ViewBag.shipProvinceCode = new SelectList(db.Provinces, "provinceCode", "provinceCode", profile.shipProvinceCode);
            ViewBag.billProvinceCode = new SelectList(db.Provinces, "provinceCode", "provinceCode", profile.billProvinceCode);
            return View(profile);
        }

        // GET Profile/Befriend/5
        public ActionResult Befriend(int? id)
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
            FriendList friendList = db.FriendLists.Where(fl => fl.personId == account.personId && fl.friendId == id).SingleOrDefault();
            if (friendList != null)
            {
                TempData["infoMsg"] = "You have already added this user as friend.";
                return RedirectToAction("");
            }
            try
            {
                friendList = new FriendList();
                friendList.personId = account.personId;
                friendList.friendId = (int)id;
                db.FriendLists.Add(friendList);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("", new { userName = account.userName });
        }

        // GET Profile/Unfriend/5
        public ActionResult Unfriend(int? id)
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
            FriendList friendList = db.FriendLists.Where(fl => fl.personId == account.personId && fl.friendId == id).SingleOrDefault();
            if (friendList == null)
            {
                TempData["infoMsg"] = "You must have already befriended this user.";
                return RedirectToAction("");
            }
            try
            {
                db.FriendLists.Remove(friendList);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("", new { userName = account.userName });
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}
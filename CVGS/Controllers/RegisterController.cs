using CVGS.Enumerations;
using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class RegisterController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Register
        public ActionResult Index()
        {
            if (Session["account"] == null)
            {
                ViewBag.genders = new SelectList(Enum.GetValues(typeof(PersonEnums.Gender)));
                ViewBag.creditCardTypes = new SelectList(Enum.GetValues(typeof(CreditCardEnums.CreditCardType)));
                ViewBag.shipProvinceCode = new SelectList(db.Provinces, "provinceCode", "provinceCode");
                ViewBag.billProvinceCode = new SelectList(db.Provinces, "provinceCode", "provinceCode");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Register
        [HttpPost]
        public ActionResult Index([Bind(Include = "userName,password,confirmedPassword,fName,mName,lName,dob,gender,phone,email,acceptEmails,number,name,expDate,creditCardType,CVV,billStreet,billCity,billPostalCode,billProvinceCode,shipStreet,shipCity,shipPostalCode,shipProvinceCode")] Profile profile)
        {
            var accnt = db.Accounts.Where(a => a.userName == profile.userName).SingleOrDefault();
            if (accnt != null)
            {
                ModelState.AddModelError("userName", "Username already exits.");
            }
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
                    Person person = new Person();
                    person.fName = profile.fName;
                    person.mName = profile.mName;
                    person.lName = profile.lName;
                    person.dob = profile.dob;
                    person.dob = profile.dob;
                    person.gender = profile.gender;
                    person.phone = profile.phone;
                    person.email = profile.email;
                    person.acceptEmails = profile.acceptEmails;
                    person.regDate = DateTime.Now;
                    db.People.Add(person);
                    db.SaveChanges();
                    Account account = new Account();
                    account.personId = person.personId;
                    account.userName = profile.userName;
                    byte[] hash = SHA256Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(profile.password));
                    StringBuilder password = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        password.Append(hash[i].ToString("X2"));
                    }
                    account.password = password.ToString();
                    account.roleCode = "member";
                    db.Accounts.Add(account);
                    CreditCard creditCard = new CreditCard();
                    creditCard.personId = person.personId;
                    creditCard.number = profile.number;
                    creditCard.name = profile.name;
                    creditCard.expDate = profile.expDate;
                    creditCard.creditCardType = profile.creditCardType;
                    creditCard.CVV = profile.CVV;
                    db.CreditCards.Add(creditCard);
                    Address shipAddress = new Address();
                    shipAddress.street = profile.shipStreet;
                    shipAddress.city = profile.shipCity;
                    shipAddress.postalCode = profile.shipPostalCode;
                    shipAddress.provinceCode = profile.shipProvinceCode;
                    Address billAddress = new Address();
                    billAddress.street = profile.billStreet;
                    billAddress.city = profile.billCity;
                    billAddress.postalCode = profile.billPostalCode;
                    billAddress.provinceCode = profile.billProvinceCode;
                    db.Addresses.Add(shipAddress);
                    db.Addresses.Add(billAddress);
                    db.SaveChanges();
                    PersonAddress shipPersonAddress = new PersonAddress();
                    shipPersonAddress.personId = person.personId;
                    shipPersonAddress.addressId = shipAddress.addressId;
                    shipPersonAddress.type = "shipping";
                    PersonAddress billPersonAddress = new PersonAddress();
                    billPersonAddress.personId = person.personId;
                    billPersonAddress.addressId = billAddress.addressId;
                    billPersonAddress.type = "billing";
                    db.PersonAddresses.Add(billPersonAddress);
                    db.PersonAddresses.Add(shipPersonAddress);
                    Cart cart = new Cart();
                    cart.personId = person.personId;
                    cart.total = 0.00m;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
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
    }
}
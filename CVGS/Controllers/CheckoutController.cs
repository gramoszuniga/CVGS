using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class CheckoutController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Checkout
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            ViewBag.subTotal = 0.00m;
            ViewBag.shipping = 0.00m;
            foreach (CartDetail cartDetail in db.CartDetails.Where(c => c.personId == account.personId))
            {
                ViewBag.subTotal += cartDetail.GameDetail.price * cartDetail.quantity;
                if (cartDetail.GameDetail.phyCopy)
                {
                    ViewBag.shipping = 12.00m;
                }
            }
            ViewBag.subTotal = account.roleCode == "employee" ? ViewBag.subTotal * (1 - account.Role.disPct) : ViewBag.subTotal;
            ViewBag.tax = (ViewBag.subTotal + ViewBag.shipping) * (db.People.Find(account.personId).PersonAddresses.Where(a => a.type == "shipping").SingleOrDefault().Address.Province.provTax - 1);
            ViewBag.grandTotal = ViewBag.subTotal + ViewBag.shipping + ViewBag.tax;
            Profile profile = new ViewModels.Profile();
            CreditCard creditCard = db.CreditCards.Where(cc => cc.Person.personId == account.personId).SingleOrDefault();
            profile.number = creditCard.number;
            profile.name = creditCard.name;
            profile.expDate = creditCard.expDate;
            profile.creditCardType = creditCard.creditCardType;
            profile.CVV = creditCard.CVV;
            Address billAddress = db.PersonAddresses.Where(pa => pa.personId == account.personId && pa.type == "billing").SingleOrDefault().Address;
            profile.billStreet = billAddress.street;
            profile.billCity = billAddress.city;
            profile.billPostalCode = billAddress.postalCode;
            profile.billProvinceCode = billAddress.provinceCode;
            Address shipAddress = db.PersonAddresses.Where(pa => pa.personId == account.personId && pa.type == "shipping").SingleOrDefault().Address;
            profile.shipStreet = shipAddress.street;
            profile.shipCity = shipAddress.city;
            profile.shipPostalCode = shipAddress.postalCode;
            profile.shipProvinceCode = shipAddress.provinceCode;
            return View(profile);
        }
    }
}
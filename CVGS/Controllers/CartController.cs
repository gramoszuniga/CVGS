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
using System.Globalization;

namespace CVGS.Controllers
{
    public class CartController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Cart
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            List<CartViewModel> cartItems = new List<CartViewModel>();
            ViewBag.total = 0;
            foreach (CartDetail cartDetail in db.CartDetails.Where(c => c.personId == account.personId))
            {
                CartViewModel cartItem = new CartViewModel();
                cartItem.cartDetailId = cartDetail.cartDetailId;
                cartItem.personId = account.personId;
                cartItem.gameDetaild = cartDetail.gameDetaild;
                cartItem.title = cartDetail.GameDetail.Game.title;
                cartItem.quantity = cartDetail.quantity;
                cartItem.price = cartDetail.GameDetail.price;
                cartItem.itemTotal = cartItem.price * cartDetail.quantity;
                ViewBag.total += cartItem.itemTotal;
                cartItems.Add(cartItem);
            }
            if (account.roleCode == "employee")
            {
                ViewBag.disPct = String.Format("{0:P2}", account.Role.disPct);
                ViewBag.disAmount = ViewBag.total * account.Role.disPct;
                ViewBag.total = ViewBag.total - ViewBag.disAmount;
            }
            return View(cartItems);
        }

        // GET: Cart/Add/5
        public ActionResult Add(int? id)
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
            CartDetail cartDetail = db.CartDetails.Where(c => c.gameDetaild == id && c.personId == account.personId).SingleOrDefault();
            if (cartDetail != null)
            {
                TempData["infoMsg"] = "You have already added this game.";
                return RedirectToAction("");
            }
            try
            {
                cartDetail = new CartDetail();
                cartDetail.personId = account.personId;
                cartDetail.gameDetaild = (int)id;
                cartDetail.quantity = 1;
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("");
        }

        // GET: Cart/Remove/5
        public ActionResult Remove(int? id)
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
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                TempData["infoMsg"] = "You must have already added this game.";
                return RedirectToAction("");
            }
            try
            {
                db.CartDetails.Remove(cartDetail);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("");
        }

        // GET: Cart/AddQuantity/5
        public ActionResult AddQuantity(int? id)
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
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                TempData["infoMsg"] = "You must have already added this game.";
                return RedirectToAction("");
            }
            try
            {
                cartDetail.quantity += 1;
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("");
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                return RedirectToAction("");
            }
        }

        // GET: Cart/SubQuantity/5
        public ActionResult SubQuantity(int? id)
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
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail.quantity < 2)
            {
                return RedirectToAction("");
            }
            try
            {
                cartDetail.quantity -= 1;
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("");
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                return RedirectToAction("");
            }
        }

        // GET: Cart/Empty
        public ActionResult Empty()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            List<CartDetail> cartDetailItems = db.CartDetails.Where(c => c.personId == account.personId).ToList();
            if (cartDetailItems == null)
            {
                throw new HttpException(404, "Not found");
            }
            try
            {
                foreach (CartDetail CartDetailItem in cartDetailItems)
                {
                    db.CartDetails.Remove(CartDetailItem);
                }
                db.SaveChanges();
                return RedirectToAction("");
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                return RedirectToAction("");
            }
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
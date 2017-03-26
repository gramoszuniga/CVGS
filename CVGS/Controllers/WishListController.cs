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
    public class WishListController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: WishList/?userName=userName
        public ActionResult Index(string userName)
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            if (account.userName != userName)
            {
                if (userName == null)
                {
                    throw new HttpException(400, "Bad request");
                }
                FriendList friendList = db.FriendLists.Where(fl => fl.personId == account.personId && fl.Person.Account.userName == userName).SingleOrDefault();
                if (friendList == null)
                {
                    throw new HttpException(404, "Not found");
                }
            }
            List<ViewModels.WishList> wishListItems = new List<ViewModels.WishList>();
            foreach (Wishlist wishList in db.Wishlists.Where(wl => wl.Person.Account.userName == userName))
            {
                ViewModels.WishList wishListItem = new ViewModels.WishList();
                wishListItem.wishlistId = wishList.wishlistId;
                wishListItem.gameId = wishList.gameId;
                wishListItem.title = wishList.Game.title;
                if (wishList.Game.GameDetails.Where(gd => gd.phyCopy).SingleOrDefault() != null)
                {
                    wishListItem.phyPrice = wishList.Game.GameDetails.Where(gd => gd.phyCopy).SingleOrDefault().price;
                }
                if (wishList.Game.GameDetails.Where(gd => !gd.phyCopy).SingleOrDefault() != null)
                {
                    wishListItem.downPrice = wishList.Game.GameDetails.Where(gd => !gd.phyCopy).SingleOrDefault().price;
                }
                wishListItems.Add(wishListItem);
            }
            return View(wishListItems);
        }

        // GET: WishList/Add/5
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
            Wishlist wishList = db.Wishlists.Where(wl => wl.gameId == id && wl.personId == account.personId).SingleOrDefault();
            if (wishList != null)
            {
                TempData["infoMsg"] = "You have already added this game.";
                return RedirectToAction("", new { userName = account.userName});
            }
            try
            {
                wishList = new Wishlist();
                wishList.personId = account.personId;
                wishList.gameId = (int)id;
                db.Wishlists.Add(wishList);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("", "WishList", new { userName = account.userName });
        }

        // GET: WishList/Remove/5
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
            Wishlist wishList = db.Wishlists.Where(wl => wl.gameId == id && wl.personId == account.personId).SingleOrDefault();
            if (wishList == null)
            {
                TempData["infoMsg"] = "You must have already added this game.";
                return RedirectToAction("", "WishList", new { userName = account.userName });
            }
            try
            {
                db.Wishlists.Remove(wishList);
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            return RedirectToAction("", "WishList", new { userName = account.userName });
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
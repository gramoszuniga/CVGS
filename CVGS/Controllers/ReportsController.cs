using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class ReportsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Reports
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
            ViewBag.games = new SelectList(db.Games.OrderBy(g => g.title), "gameId", "title");
            ViewBag.people = new SelectList(db.Accounts.OrderBy(a => a.userName), "personId", "userName");
            return View();
        }

        // GET: Reports/Games      
        public ActionResult Games()
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
            List<GameAndGameDetails> gameAndGameDetailsList = new List<GameAndGameDetails>();
            foreach (GameDetail gameDetail in db.GameDetails)
            {
                GameAndGameDetails gameAndGameDetails = new GameAndGameDetails();
                gameAndGameDetails.gameDetailId = gameDetail.gameDetailId;
                gameAndGameDetails.gameId = gameDetail.gameId;
                gameAndGameDetails.title = gameDetail.Game.title;
                gameAndGameDetails.relDate = gameDetail.Game.relDate;
                gameAndGameDetails.desc = gameDetail.Game.desc;
                gameAndGameDetails.cover = gameDetail.Game.cover;
                gameAndGameDetails.publisher = gameDetail.Game.publisher;
                gameAndGameDetails.rateAVG = gameDetail.Game.rateAVG;
                gameAndGameDetails.phyCopy = gameDetail.phyCopy;
                gameAndGameDetails.price = gameDetail.price;
                gameAndGameDetails.qoh = gameDetail.phyCopy ? gameDetail.qoh : null;
                gameAndGameDetails.genreCode = gameDetail.Game.GameGenres.First().genreCode;
                gameAndGameDetails.platformCode = gameDetail.Game.GamePlatforms.First().platformCode;
                gameAndGameDetailsList.Add(gameAndGameDetails);
            }
            return View(gameAndGameDetailsList.OrderBy(g => g.title));
        }

        // GET: Reports/Game
        public ActionResult Game(int? gameId)
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
            if (gameId == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Game game = db.Games.Find(gameId);
            if (game == null)
            {
                throw new HttpException(404, "Not found");
            }
            GameAndGameDetails gameAndGameDetails = new GameAndGameDetails();
            gameAndGameDetails.gameId = game.gameId;
            gameAndGameDetails.title = game.title;
            gameAndGameDetails.publisher = game.publisher;
            gameAndGameDetails.relDate = game.relDate;
            gameAndGameDetails.rateAVG = game.rateAVG;
            GameDetail physical = game.GameDetails.Where(g => g.phyCopy).SingleOrDefault();
            GameDetail downloadable = game.GameDetails.Where(g => !g.phyCopy).SingleOrDefault();
            List<OrderDetail> orders;
            if (physical != null)
            {
                ViewBag.phyPrice = physical.price;
                gameAndGameDetails.qoh = physical.qoh;
                orders = db.OrderDetails.Where(o => o.gameDetailId == physical.gameDetailId).ToList();
                ViewBag.buys = orders != null ? orders.Count : 0;
            }
            if (downloadable != null)
            {
                ViewBag.downPrice = downloadable.price;
                orders = db.OrderDetails.Where(o => o.gameDetailId == downloadable.gameDetailId).ToList();
                ViewBag.downloads = orders != null ? orders.Count : 0;
            }
            gameAndGameDetails.genreCode = game.GameGenres.First().genreCode;
            return View(gameAndGameDetails);
        }

        // GET: Reports/Members
        public ActionResult Members()
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
            return View(db.People.ToList());
        }

        // GET: Reports/Member
        public ActionResult Member(int? personId)
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
            if (personId == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Person person = db.People.Find(personId);
            if (person == null)
            {
                throw new HttpException(404, "Not found");
            }
            List<OrderDetail> physicals = db.OrderDetails.Where(od => od.Order.personId == personId && od.GameDetail.phyCopy).ToList();
            List<OrderDetail> downloads = db.OrderDetails.Where(od => od.Order.personId == personId && !od.GameDetail.phyCopy).ToList();
            ViewBag.buys = physicals != null ? physicals.Count : 0;
            ViewBag.downloads = downloads != null ? downloads.Count : 0;
            Address billAddress = person.PersonAddresses.Where(pa => pa.personId == person.personId && pa.type == "billing").SingleOrDefault().Address;
            Address shipAddress = person.PersonAddresses.Where(pa => pa.personId == person.personId && pa.type == "shipping").SingleOrDefault().Address;
            ViewBag.shipAddress = shipAddress.street + ", " + shipAddress.city;
            ViewBag.billAddress = billAddress.street + ", " + billAddress.city;
            return View(person);
        }

        // GET: Reports/WishList
        public ActionResult WishList(int? personId)
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
            List<ViewModels.WishList> wishListItems = new List<ViewModels.WishList>();
            foreach (Wishlist wishList in db.Wishlists.Where(wl => wl.Person.personId == personId))
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
            ViewBag.userName = db.People.Find(personId).Account.userName;
            return View(wishListItems);
        }

        // GET: Reports/Sales
        public ActionResult Sales()
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
            return View(db.Orders.ToList());
        }
    }
}
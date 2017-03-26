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
using CVGS.Enumerations;

namespace CVGS.Controllers
{
    public class GamesController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Games
        public ActionResult Index(string searchString)
        {
            List<GameDetail> gameDetails = db.GameDetails.Where(g => g.qoh != 0).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                gameDetails = gameDetails.Where(g => g.Game.title.ToLower().Contains(searchString) || g.Game.GamePlatforms.First().platformCode.ToLower().Contains(searchString) || g.Game.GameGenres.First().genreCode.ToLower().Contains(searchString)).ToList();
            }
            List<GameAndGameDetails> gameAndGameDetailsList = new List<GameAndGameDetails>();
            foreach (GameDetail gameDetail in gameDetails)
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
            return View(gameAndGameDetailsList);
        }

        // GET: MyGames
        public ActionResult MyGames()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            List<GameAndGameDetails> gameAndGameDetailsList = new List<GameAndGameDetails>();
            foreach (OrderDetail orderDetail in db.OrderDetails.Where(od => od.Order.Person.Account.userName == account.userName))
            {
                GameAndGameDetails gameAndGameDetails = new GameAndGameDetails();
                gameAndGameDetails.gameDetailId = orderDetail.GameDetail.gameDetailId;
                gameAndGameDetails.gameId = orderDetail.GameDetail.gameId;
                gameAndGameDetails.title = orderDetail.GameDetail.Game.title;
                gameAndGameDetails.relDate = orderDetail.GameDetail.Game.relDate;
                gameAndGameDetails.desc = orderDetail.GameDetail.Game.desc;
                gameAndGameDetails.cover = orderDetail.GameDetail.Game.cover;
                gameAndGameDetails.publisher = orderDetail.GameDetail.Game.publisher;
                gameAndGameDetails.rateAVG = orderDetail.GameDetail.Game.rateAVG;
                gameAndGameDetails.phyCopy = orderDetail.GameDetail.phyCopy;
                gameAndGameDetails.price = orderDetail.GameDetail.price;
                gameAndGameDetails.qoh = orderDetail.GameDetail.phyCopy ? orderDetail.GameDetail.qoh : null;
                gameAndGameDetails.genreCode = orderDetail.GameDetail.Game.GameGenres.First().genreCode;
                gameAndGameDetails.platformCode = orderDetail.GameDetail.Game.GamePlatforms.First().platformCode;
                gameAndGameDetailsList.Add(gameAndGameDetails);
            }
            return View(gameAndGameDetailsList);
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            GameDetail gameDetail = db.GameDetails.Find(id);
            if (gameDetail == null)
            {
                throw new HttpException(404, "Not found");
            }
            Game game = gameDetail.Game;
            if (game == null)
            {
                throw new HttpException(404, "Not found");
            }
            GameAndGameDetails gameAndGameDetails = new GameAndGameDetails();
            gameAndGameDetails.gameDetailId = gameDetail.gameDetailId;
            gameAndGameDetails.gameId = gameDetail.Game.gameId;
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
            gameAndGameDetails.Reviews = gameDetail.Game.Reviews.Where(r => r.status == ReviewEnums.ReviewStatus.Approved.ToString()).ToList();
            return View(gameAndGameDetails);
        }

        // GET: Games/Create
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
            ViewBag.genres = new SelectList(db.Genres, "genreCode", "genreCode");
            ViewBag.platforms = new SelectList(db.Platforms, "platformCode", "platformCode");
            ViewBag.publishers = new SelectList(Enum.GetValues(typeof(GameEnums.Publisher)));
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,platformCode,publisher,price,qoh,relDate,genreCode,desc,phyCopy")] GameAndGameDetails gameAndGameDetails)
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
            Game game = db.Games.Where(g => g.title == gameAndGameDetails.title).SingleOrDefault();
            if (game == null)
            {
                if (gameAndGameDetails.relDate > DateTime.Today)
                {
                    ModelState.AddModelError("relDate", "Release date cannot be later than today.");
                }
                if (gameAndGameDetails.phyCopy && gameAndGameDetails.qoh == null)
                {
                    ModelState.AddModelError("qoh", "Quantity on hand is required.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        game = new Game();
                        game.title = gameAndGameDetails.title;
                        game.relDate = gameAndGameDetails.relDate;
                        game.desc = gameAndGameDetails.desc;
                        game.publisher = gameAndGameDetails.publisher;
                        db.Games.Add(game);
                        db.SaveChanges();
                        GameDetail gameDetail = new GameDetail();
                        gameDetail.gameId = game.gameId;
                        gameDetail.phyCopy = gameAndGameDetails.phyCopy;
                        gameDetail.price = gameAndGameDetails.price;
                        if (gameDetail.phyCopy)
                        {
                            gameDetail.qoh = gameAndGameDetails.qoh;
                        }
                        else
                        {
                            gameDetail.qoh = null;
                        }
                        GamePlatform gamePlatform = new GamePlatform();
                        gamePlatform.gameId = game.gameId;
                        gamePlatform.platformCode = gameAndGameDetails.platformCode;
                        GameGenre gameGenre = new GameGenre();
                        gameGenre.gameId = game.gameId;
                        gameGenre.genreCode = gameAndGameDetails.genreCode;
                        db.GameDetails.Add(gameDetail);
                        db.GamePlatforms.Add(gamePlatform);
                        db.GameGenres.Add(gameGenre);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                    }
                }
            }
            else
            {
                if (game.GameDetails.Count > 1)
                {
                    ModelState.AddModelError("title", "Both a physical and downloadable copy of this game have already been created.");
                }
                else
                {
                    if (gameAndGameDetails.relDate > DateTime.Today)
                    {
                        ModelState.AddModelError("relDate", "Release date cannot be later than today.");
                    }
                    if (game.GameDetails.SingleOrDefault() != null && game.GameDetails.SingleOrDefault().phyCopy == gameAndGameDetails.phyCopy)
                    {
                        if (gameAndGameDetails.phyCopy)
                        {
                            ModelState.AddModelError("phyCopy", "A physical copy of this game has already been created.");
                        }
                        else
                        {
                            ModelState.AddModelError("phyCopy", "A downloadable copy of this game has already been created.");
                        }
                    }
                    else
                    {
                        if (gameAndGameDetails.phyCopy && gameAndGameDetails.qoh == null)
                        {
                            ModelState.AddModelError("qoh", "Quantity on hand is required.");
                        }
                        if (ModelState.IsValid)
                        {
                            try
                            {
                                GameDetail gameDetail = new GameDetail();
                                gameDetail.gameId = game.gameId;
                                gameDetail.phyCopy = gameAndGameDetails.phyCopy;
                                gameDetail.price = gameAndGameDetails.price;
                                if (gameDetail.phyCopy)
                                {
                                    gameDetail.qoh = gameAndGameDetails.qoh;
                                }
                                else
                                {
                                    gameDetail.qoh = null;
                                }
                                db.GameDetails.Add(gameDetail);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            catch (Exception)
                            {
                                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                            }
                        }
                    }
                }
            }
            ViewBag.genres = new SelectList(db.Genres, "genreCode", "genreCode", gameAndGameDetails.genreCode);
            ViewBag.platforms = new SelectList(db.Platforms, "platformCode", "platformCode", gameAndGameDetails.platformCode);
            ViewBag.publishers = new SelectList(Enum.GetValues(typeof(GameEnums.Publisher)), gameAndGameDetails.publisher);
            return View(gameAndGameDetails);
        }

        // GET: Games/Edit/5
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
            Game game = db.GameDetails.Find(id).Game;
            GameDetail gameDetail = db.GameDetails.Find(id);
            if (game == null)
            {
                throw new HttpException(404, "Not found");
            }
            if (gameDetail == null)
            {
                throw new HttpException(404, "Not found");
            }

            GameAndGameDetails gameAndGameDetails = new GameAndGameDetails();
            gameAndGameDetails.gameDetailId = gameDetail.gameDetailId;
            gameAndGameDetails.gameId = gameDetail.Game.gameId;
            gameAndGameDetails.title = gameDetail.Game.title;
            gameAndGameDetails.relDate = gameDetail.Game.relDate;
            gameAndGameDetails.desc = gameDetail.Game.desc;
            gameAndGameDetails.publisher = gameDetail.Game.publisher;
            gameAndGameDetails.price = gameDetail.price;
            gameAndGameDetails.phyCopy = gameDetail.phyCopy;
            gameAndGameDetails.qoh = gameDetail.phyCopy ? gameDetail.qoh : null;
            gameAndGameDetails.genreCode = gameDetail.Game.GameGenres.First().genreCode;
            gameAndGameDetails.platformCode = gameDetail.Game.GamePlatforms.First().platformCode;
            ViewBag.genres = new SelectList(db.Genres, "genreCode", "genreCode", gameAndGameDetails.genreCode);
            ViewBag.platforms = new SelectList(db.Platforms, "platformCode", "platformCode", gameAndGameDetails.platformCode);
            ViewBag.publishers = new SelectList(Enum.GetValues(typeof(GameEnums.Publisher)), gameAndGameDetails.publisher);
            return View(gameAndGameDetails);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gameDetailId,gameId,title,platformCode,publisher,price,qoh,relDate,genreCode,desc")] GameAndGameDetails gameAndGameDetails)
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
            if (gameAndGameDetails.relDate > DateTime.Today)
            {
                ModelState.AddModelError("relDate", "Release date cannot be later than today.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Game game = db.Games.Find(gameAndGameDetails.gameId);
                    game.title = gameAndGameDetails.title;
                    game.relDate = gameAndGameDetails.relDate;
                    game.desc = gameAndGameDetails.desc;
                    game.publisher = gameAndGameDetails.publisher;
                    db.Entry(game).State = EntityState.Modified;
                    GameDetail gameDetail = db.GameDetails.Find(gameAndGameDetails.gameDetailId);
                    gameDetail.price = (decimal)gameAndGameDetails.price;
                    gameDetail.qoh = gameAndGameDetails.phyCopy ? gameAndGameDetails.qoh : null;
                    db.Entry(gameDetail).State = EntityState.Modified;
                    GamePlatform gamePlatform = db.GamePlatforms.Where(gp => gp.gameId == gameAndGameDetails.gameId).SingleOrDefault();
                    gamePlatform.platformCode = gameAndGameDetails.platformCode;
                    GameGenre gameGenre = db.GameGenres.Where(gg => gg.gameId == gameAndGameDetails.gameId).SingleOrDefault();
                    gameGenre.genreCode = gameAndGameDetails.genreCode;
                    db.Entry(gamePlatform).State = EntityState.Modified;
                    db.Entry(gameGenre).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ViewBag.genres = new SelectList(db.Genres, "genreCode", "genreCode", gameAndGameDetails.genreCode);
            ViewBag.platforms = new SelectList(db.Platforms, "platformCode", "platformCode", gameAndGameDetails.platformCode);
            ViewBag.publishers = new SelectList(Enum.GetValues(typeof(GameEnums.Publisher)), gameAndGameDetails.publisher);
            return View(gameAndGameDetails);
        }

        // GET: Games/Delete/5
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
            Game game = db.GameDetails.Find(id).Game;
            GameDetail gameDetail = db.GameDetails.Find(id);
            if (game == null)
            {
                throw new HttpException(404, "Not found");
            }
            if (gameDetail == null)
            {
                throw new HttpException(404, "Not found");
            }
            return View(game);
        }

        // POST: Games/Delete/5
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
            GameDetail gameDetail = db.GameDetails.Find(id);
            gameDetail.qoh = 0;
            db.Entry(gameDetail).State = EntityState.Modified;
            foreach (CartDetail cartDetail in db.CartDetails.Where(c => c.gameDetaild == gameDetail.gameDetailId))
            {
                db.CartDetails.Remove(cartDetail);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Games/Download
        public ActionResult Download()
        {
            throw new HttpException(404, "Not found");
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CVGS.Models;
using CVGS.Enumerations;

namespace CVGS.Controllers
{
    public class OrdersController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Person);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(400, "Bad request");
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                throw new HttpException(404, "Not found");
            }
            var orderDetails = db.OrderDetails.Include(o => o.GameDetail).Include(o => o.Order).Where(o => o.orderId == id);
            return View(orderDetails.ToList());
        }

        // GET: Orders/Add
        public ActionResult Add()
        {
            Account account = Session["account"] as Account;
            if (account == null)
            {
                TempData["infoMsg"] = "You must be logged in.";
                return RedirectToAction("", "Login");
            }
            try
            {
                Order order = new Order();
                order.personId = account.personId;
                order.ordDate = DateTime.Today;
                order.status = OrderEnums.OrderStatus.Pending.ToString();
                order.total = 0.00m;
                db.Orders.Add(order);
                db.SaveChanges();
                List<CartDetail> cartDetails = db.CartDetails.Where(c => c.personId == account.personId).ToList();
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (CartDetail cartDetail in cartDetails)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.orderId = order.orderId;
                    orderDetail.gameDetailId = cartDetail.gameDetaild;
                    orderDetail.quantity = cartDetail.quantity;
                    orderDetail.uPrice = cartDetail.GameDetail.price;
                    order.total += orderDetail.uPrice;
                    orderDetails.Add(orderDetail);
                    db.CartDetails.Remove(cartDetail);
                }
                foreach (OrderDetail orderDetail in orderDetails)
                {
                    db.OrderDetails.Add(orderDetail);
                }
                db.SaveChanges();
                return RedirectToAction("MyGames", "Games");
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                return RedirectToAction("", "Checkout");
            }
        }

        // GET: Orders/Edit/5
        public ActionResult UpdateStatus(int? id)
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
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                throw new HttpException(404, "Not found");
            }
            ViewBag.statuses = new SelectList(Enum.GetValues(typeof(OrderEnums.OrderStatus)), order.status);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus([Bind(Include = "orderId,status")] Order order)
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
                Order rdr = db.Orders.Find(order.orderId);
                rdr.status = order.status;
                db.Entry(rdr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("");
            }
            catch (Exception)
            {
                TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
            }
            ViewBag.statuses = new SelectList(Enum.GetValues(typeof(ReviewEnums.ReviewStatus)), order.status);
            return View(order);
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
using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class LoginController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Login
        public ActionResult Index()
        {
            if (Session["account"] == null)
            {
                return View();
            }
            return RedirectToAction("", "Home");
        }

        // POST: Login
        [HttpPost]
        public ActionResult Index([Bind(Include = "userName,password")] AccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account accnt = db.Accounts.Where(a => a.userName == account.userName).SingleOrDefault();
                    if (accnt == null)
                    {
                        ModelState.AddModelError("password", "Username or password are invalid.");
                        return View(account);
                    }
                    byte[] hash = SHA256Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(account.password));
                    StringBuilder password = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        password.Append(hash[i].ToString("X2"));
                    }
                    if (!accnt.isLocked)
                    {
                        if (accnt.password != password.ToString() && accnt.loginAttempts < 3)
                        {
                            accnt.loginAttempts += 1;
                            db.Entry(accnt).State = EntityState.Modified;
                            db.SaveChanges();
                            ModelState.AddModelError("password", "Username or password are invalid.");
                        }
                        else if (accnt.password != password.ToString() && accnt.loginAttempts > 2)
                        {
                            accnt.isLocked = true;
                            db.Entry(accnt).State = EntityState.Modified;
                            db.SaveChanges();
                            ModelState.AddModelError("userName", "Account is locked.");
                        }
                        else
                        {
                            accnt.loginAttempts = 0;
                            db.Entry(accnt).State = EntityState.Modified;
                            db.SaveChanges();
                            Session["account"] = accnt;
                            return RedirectToAction("", "Home");
                        }
                        return View(account);
                    }
                    ModelState.AddModelError("userName", "Account is locked.");
                }
                catch (Exception err)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            return View(account);
        }
    }
}
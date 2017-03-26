using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class RecoverPasswordController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: RecoverPassword
        public ActionResult Index()
        {
            return View();
        }

        // POST: RecoverPassword
        [HttpPost]
        public ActionResult Index([Bind(Include = "userName,email")] PasswordRecoverer passwordRecoverer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account account = db.Accounts.Where(a => a.userName == passwordRecoverer.userName && a.Person.email == passwordRecoverer.email).SingleOrDefault();
                    if (account != null)
                    {
                        // Here goes the logics to send e-mail for password recovery
                        return RedirectToAction("", "Login");
                    }
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ModelState.AddModelError("email", "Username or e-mail are not valid.");
            return View(passwordRecoverer);
        }
    }
}
using CVGS.Models;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class ChangePasswordController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: ChangePassword
        public ActionResult Index()
        {
            if (Session["account"] != null)
            {
                return View();
            }
            TempData["infoMsg"] = "You must be logged in.";
            return RedirectToAction("", "Login");
        }

        // POST: ChangePassword
        [HttpPost]
        public ActionResult Index([Bind(Include = "currentPassword,newPassword,confirmedNewPassword")] PasswordChanger changePassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Account accnt = db.Accounts.Find((Session["account"] as Account).personId);
                    byte[] hash = SHA256Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(changePassword.currentPassword));
                    StringBuilder currentPassword = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        currentPassword.Append(hash[i].ToString("X2"));
                    }
                    if (accnt.password == currentPassword.ToString())
                    {
                        hash = SHA256Managed.Create().ComputeHash(Encoding.UTF8.GetBytes(changePassword.newPassword));
                        StringBuilder newPassword = new StringBuilder();
                        for (int i = 0; i < hash.Length; i++)
                        {
                            newPassword.Append(hash[i].ToString("X2"));
                        }
                        accnt.password = newPassword.ToString();
                        db.Entry(accnt).State = EntityState.Modified;
                        db.SaveChanges();
                        Session.Remove("account");
                        TempData["infoMsg"] = "Password successfully changed.";
                        return RedirectToAction("", "Login");
                    }
                }
                catch (Exception)
                {
                    TempData["errMsg"] = "An unexpected error has occurred. Please try again later.";
                }
            }
            ModelState.AddModelError("currentPassword", "Current password is not valid.");
            return View(changePassword);
        }
    }
}
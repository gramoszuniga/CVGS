using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CVGS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }

        // GET: Error/NotFound
        public ActionResult BadRequest()
        {
            return View();
        }

        // GET: Error/NotFound
        public ActionResult AccessDenied()
        {
            return View();
        }

        // GET: Error/NotFound
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
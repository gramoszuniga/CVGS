using CVGS.Binders;
using CVGS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CVGS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());
            ModelBinders.Binders.Add(typeof(Profile), new ProfileBinder());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is HttpException)
            {
                if (((HttpException)ex).GetHttpCode() == 400)
                {
                    Response.Redirect("~/Error/BadRequest");
                }
                if (((HttpException)ex).GetHttpCode() == 403)
                {
                    Response.Redirect("~/Error/AccessDenied");
                }
                if (((HttpException)ex).GetHttpCode() == 404)
                {
                    Response.Redirect("~/Error/NotFound");
                }
            }
        }
    }
}
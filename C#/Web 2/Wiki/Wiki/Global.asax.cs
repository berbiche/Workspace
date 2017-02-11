using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Threading;
using System.Globalization;

namespace Wiki
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["langue"];
            if (cookie == null)
            {
                string langue = Request.UserLanguages?[0];
                try
                {
                    Thread.CurrentThread.CurrentCulture = langue == null
                        ? CultureInfo.InvariantCulture
                        : new CultureInfo(langue);
                }
                catch (CultureNotFoundException)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                }
            }
            else
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
                }
                catch (CultureNotFoundException)
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                }
            }

            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }


    }
}
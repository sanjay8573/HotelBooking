using HotelBooking.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HotelBooking
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Logger l = new Logger();
            //Response.Write("Somthing went wrong....");
            //Response.Write(Server.GetLastError().ToString());
            //Response.Write("========");
            //Response.Write(Server.GetLastError().Message.ToString());
            l.Log(Server.GetLastError().ToString(), Server.GetLastError().Message.ToString(), DateTime.Now);
            Server.ClearError();
        }

    }
}

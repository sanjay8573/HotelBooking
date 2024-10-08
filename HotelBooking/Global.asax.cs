﻿using HotelBooking.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "MM-dd-yy";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var msg = Server.GetLastError().Message.ToString() + ex.StackTrace.ToString();
            Logger l = new Logger();
            Server.ClearError();
            l.Log(ex.ToString(), msg, DateTime.Now);
            //Response.Redirect("~/Error.cshtml");
        }

    }
}

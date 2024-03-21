using HotelBooking.Controllers;
using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;

namespace HotelBooking.Attribute
{
    public class CheckSessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (HttpContext.Current.Session["BranchId"] == null)
            {
               
                LoginController ll = new LoginController();

                HttpCookie uu = HttpContext.Current.Request.Cookies.Get("LoginCookies");
                if(uu == null)
                {
                    filterContext.Result = new RedirectResult("/UnProHome/LogOff");
                }
                else
                {
                    int uid = int.Parse(Helper.HotelBookingHelper.Base64Decode(uu.Value.ToString()));
                    //int uid = getValuefromformAuthCookie();
                    UserLoginResponse model = ll.UserLogin(uid);
                    HttpContext.Current.Session["Logged_In"] = "LoggedIn";
                    HttpContext.Current.Session["UserLoginResponse"] = model;
                    HttpContext.Current.Session["CompanyId"] = model.CompanyId;
                    HttpContext.Current.Session["BranchId"] = model.BranchId;
                    //filterContext.Result = new RedirectResult("~/UnProHome/LogOff");
                    return;
                }
               
                
            }
            base.OnActionExecuting(filterContext);
        }
            
    }
}
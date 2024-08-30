using HotelBooking.Controllers;
using HotelBooking.Model;
using HotelBooking.Repository.Implementation;
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
                    //Logger _l = new Logger();
                   
                    
                    int uid = int.Parse(Helper.HotelBookingHelper.Base64Decode(uu.Value.ToString()));
                    //_l.Log("CheckSessionTimeOut", "UserId" + uid.ToString(), DateTime.Now);
                    UserLoginResponse model = ll.UserLogin(uid);
                    HttpContext.Current.Session["Logged_In"] = "LoggedIn";
                    HttpContext.Current.Session["UserLoginResponse"] = model;
                    HttpContext.Current.Session["CompanyId"] = model.CompanyId;
                    HttpContext.Current.Session["BranchId"] = model.BranchId;
                    HttpContext.Current.Session["BranchCurrencyName"] = model.BranchCurrencyName;
                    HttpContext.Current.Session["BranchCurrencyCode"] = model.BranchCurrencyCode;
                    HttpContext.Current.Session["BranchCurrencySymbol"] = model.BranchCurrencySymbol;
                    HttpContext.Current.Session["BranchTax"] = model.BranchTaxPercentage;
                    HttpContext.Current.Session["Logged_In_User"] = model.UserId;
                    //_l.Log("CheckSessionTimeOut", "Session Created from CookieUserId" + uid.ToString(), DateTime.Now);
                    //filterContext.Result = new RedirectResult("~/UnProHome/LogOff");
                    return;
                }


            }
            
            base.OnActionExecuting(filterContext);
        }
            
    }
}
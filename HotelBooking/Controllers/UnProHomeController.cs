using HotelBooking.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace HotelBooking.Controllers
{
    public class UnProHomeController : Controller
    {
        // GET: UnProHome
        [HttpGet]
        public ActionResult Index()
        {
            //if (Session != null && Session.Keys.Count > 0) { 
            //    ViewBag.ErrorMessage = Session["ErrorMessage"].ToString();
            //    }
            LoginRequestModel lrm= new LoginRequestModel();
            return View("login",lrm);
        }
        public ActionResult AppErr()
        {
            return View("Error");
        }
        public ActionResult LogOff()
        {
            return View("LogOff");
        }
        [HttpPost]
        public ActionResult LoginSubmit(LoginRequestModel lm)
        {

           LoginController ll= new LoginController();
           LoginResponse urm= ll.ValidateLogin(lm);
           
            
            if (urm.UserId <= 0)
            {
                Session["ErrorMessage"] = "Please check your credential";
               
                return RedirectToAction("index", "UnProHome");
            }
            else {
                
                LoginController loginController = new LoginController();
              
                UserLoginResponse model = loginController.UserLogin(urm.UserId);
                
                //FormsAuthenticationTicket fut = new FormsAuthenticationTicket(2, urm.UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Empty);
                //string encTicket = FormsAuthentication.Encrypt(fut);
                //HttpContext.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);

                //HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { HttpOnly=true});

                //FormsAuthentication.SetAuthCookie(model.UserId.ToString(), false);
                HttpContext.Session["Logged_In"] = "LoggedIn";

                HttpContext.Session["UserLoginResponse"] = model;
                HttpContext.Session["CompanyId"] = model.CompanyId;
                HttpContext.Session["BranchId"] = model.BranchId;

                HttpContext.Response.Cookies.Remove("LoginCookies");
                HttpCookie userCookies = new HttpCookie("LoginCookies");
                userCookies.Value = Helper.HotelBookingHelper.Base64Encode(urm.UserId.ToString());
                userCookies.Expires = DateTime.Now.AddHours(12);
                HttpContext.Response.Cookies.Add(userCookies);
                return RedirectToAction("index", "Home");
            }

            
        }
    }
}
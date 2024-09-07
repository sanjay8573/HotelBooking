using HotelBooking.Model;
using System;
using System.Web;
using System.Web.Mvc;
using System.Runtime;
using System.Linq;

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
                              
                HttpContext.Session["Logged_In"] = "LoggedIn";
                HttpContext.Session["UserLoginResponse"] = model;
                HttpContext.Session["CompanyId"] = model.CompanyId;
                HttpContext.Session["BranchId"] = model.BranchId;
                HttpContext.Session["BranchCurrencyName"] = model.BranchCurrencyName;
                HttpContext.Session["BranchCurrencyCode"] = model.BranchCurrencyCode;
                HttpContext.Session["BranchCurrencySymbol"] = model.BranchCurrencySymbol;
                HttpContext.Session["BranchTax"] = model.BranchTaxPercentage;
                HttpContext.Session["BranchTaxDetails"] = model.TaxDetails;
                HttpContext.Session["Logged_In_User"] = model.UserId;


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
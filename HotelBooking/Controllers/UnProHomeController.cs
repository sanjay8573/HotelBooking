using HotelBooking.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HotelBooking.Controllers
{
    public class UnProHomeController : Controller
    {
        // GET: UnProHome
        public ActionResult Index()
        {
            if (Session != null && Session.Keys.Count > 0) { 
                ViewBag.ErrorMessage = Session["ErrorMessage"].ToString();
                }
            LoginRequestModel lrm= new LoginRequestModel();
            return View("login",lrm);
        }
        public ActionResult LoginSubmit(LoginRequestModel lm)
        {

           LoginController ll= new LoginController();
           LoginResponse urm= ll.ValidateLogin(lm);
           
            Session["UserId"] = urm.UserId;
            Session["Token"] = urm.validToen;
            if (urm.UserId <= 0)
            {
                Session["ErrorMessage"] = "Please check your credential";
               
                return RedirectToAction("index", "UnProHome");
            }
            else { 
                return RedirectToAction("index", "Home");
            }

            
        }
    }
}
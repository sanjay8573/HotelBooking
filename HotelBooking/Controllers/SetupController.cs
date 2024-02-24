using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    public class SetupController : Controller
    {
        // GET: Setup
        public ActionResult Company()
        {
            return View();
        }

        public ActionResult Branch()
        {
            return View();
        }
    }
}
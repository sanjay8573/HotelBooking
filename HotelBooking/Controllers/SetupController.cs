using HotelBooking.Model;
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
        public int SaveCompay(Company companyEntity)
        {
            CompanyInfoController _bk = new CompanyInfoController();
            return _bk.AddCompanyDetails(companyEntity);
        }

        public ActionResult Branch()
        {
            return View();
        }
    }
}
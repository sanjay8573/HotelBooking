using HotelBooking.Desig;
using HotelBooking.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HotelBooking.Controllers
{
    public class HRController : Controller
    {
        // GET: HR
        public ActionResult Employees(int? page, int? pSize)
        {


            if (Session.Keys.Count != 0)
            {
                int? DefaultPageSize = 10;
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;

                ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

                int branchId = int.Parse(Session["BranchId"].ToString());
                StaffController _stf = new StaffController();
                IEnumerable<VM_Staff> stafs = _stf.GetStaffs(branchId);

                return View("Employees", stafs.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
            
           
        }

        public ActionResult Designation(int? page, int? pSize)
        {
            if (Session.Keys.Count != 0)
            {
                int? DefaultPageSize = 10;
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;

                ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

                int branchId = int.Parse(Session["BranchId"].ToString());
                DesigController _dg = new DesigController();
                IEnumerable<Designation> dg = _dg.GetAllDesignation(branchId);

                return View("Designation", dg.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult Dept(int? page, int? pSize)
        {
            if (Session.Keys.Count != 0)
            {
                int? DefaultPageSize = 10;
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;

                ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

                int branchId = int.Parse(Session["BranchId"].ToString());
                DeptController _dpt = new DeptController();
                IEnumerable<Dept> dpts = _dpt.GetDeptDetails(branchId);

                return View("Dept", dpts.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
    }
}
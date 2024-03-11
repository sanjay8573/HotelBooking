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
                        new SelectListItem() { Value="2", Text= "2" ,Selected=(2==pSize)},
                        new SelectListItem() { Value="5", Text= "5" ,Selected=(5==pSize)},
                        new SelectListItem() { Value="10", Text= "10" ,Selected=(10==pSize)},
                        new SelectListItem() { Value="15", Text= "15",Selected=(15==pSize) },
                        new SelectListItem() { Value="20", Text= "20",Selected=(20==pSize) },
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
                       new SelectListItem() { Value="2", Text= "2" ,Selected=(2==pSize)},
                        new SelectListItem() { Value="5", Text= "5" ,Selected=(5==pSize)},
                        new SelectListItem() { Value="10", Text= "10" ,Selected=(10==pSize)},
                        new SelectListItem() { Value="15", Text= "15",Selected=(15==pSize) },
                        new SelectListItem() { Value="20", Text= "20",Selected=(20==pSize) },
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
        public ActionResult AddEmployees(int EmpId = 0)
        {
            StaffController _stf = new StaffController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            VM_Staff stf=new VM_Staff();
            if (EmpId > 0)
            {
                stf = _stf.GetStaffs(branchId).Where(s => s.StaffId == EmpId).SingleOrDefault();
            }
            return View();
        }

        public ActionResult AddDept(int DeptId = 0)
        {
            DeptController   _stf = new DeptController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            Dept dpt = new Dept();
            dpt.BranchId= branchId;
            if (DeptId > 0)
            {
                dpt = _stf.GetDeptById(DeptId);
            }

            return View("AddDept",dpt);
        }
        public ActionResult SaveDept(Dept DeptEntity )
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DeptController _stf = new DeptController();
            _stf.AddDeptDetails(DeptEntity, branchId);

            return RedirectToAction("Dept", "HR");
        }

        public ActionResult DelDept(int DeptId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DeptController _stf = new DeptController();
            _stf.DeleteDepartment(DeptId);

            return RedirectToAction("Dept", "HR");
        }

        public ActionResult AddDesig(int DesigId = 0)
        {
            DesigController _stf = new DesigController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            Designation dsg = new Designation();
            dsg.BranchId = branchId;
            if (DesigId > 0)
            {
                dsg = _stf.GetDesignation(DesigId);
            }

            return View("AddDesig", dsg);
        }
        public ActionResult SaveDesig(Designation DesigEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DesigController _stf = new DesigController();
            _stf.AddDesignation(DesigEntity);

            return RedirectToAction("Designation", "HR");
        }
        public ActionResult DelDesig(int DesigId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DesigController _stf = new DesigController();
            _stf.DelDesignation(DesigId);

            return RedirectToAction("Dept", "HR");
        }


    }
}
using HotelBooking.Model;
using HotelBooking.Model.Inventory;
using HotelBooking.Repository.Implementation;
using HotelBooking.Repository.Interface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers.StoreManagement
{
    public class storeController : Controller
    {
        // GET: store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetItems(int? page, int? pSize)
        {
            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            int branchId = int.Parse(Session["BranchId"].ToString());

            StoreController str = new StoreController();
            IEnumerable<ItemMaster> ItemListModel = new List<ItemMaster>();
            ItemListModel = str.GetItems(branchId);
            IPagedList<ItemMaster> bitemlist = ItemListModel.ToPagedList(pageNumber, (int)DefaultPageSize);


            return View("storeItems", bitemlist);
        }


        public ActionResult AddItems()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            ViewBag.BranchId = branchId;
            return View("AddItems");
        }
        public ActionResult SaveItems(IEnumerable<ItemMaster> items)
        {
           
            int branchId = int.Parse(Session["BranchId"].ToString());
            StoreController _rm = new StoreController();

            bool rtnVal = _rm.AddItems(items);

            Session["BranchId"] = branchId;

            return RedirectToAction("GetItems");
        }
        public ActionResult RDItems()
        {

            int branchId = int.Parse(Session["BranchId"].ToString());

            StoreController str = new StoreController();
            UserLoginResponse usr = Session["UserLoginResponse"] as UserLoginResponse;
            ViewBag.IssueById = usr.UserId;
            ViewBag.IssueByName = usr.UserName;

            IEnumerable<ItemMaster> alltem = str.GetItems(branchId);

            List<SelectListItem> dptitems = new List<SelectListItem>();
            dptitems.Add(new SelectListItem { Text = "Select a Item", Value = "0" });
            foreach (var item in alltem)
            {
               
                dptitems.Add(new SelectListItem { Text = item.ItemName, Value = item.ItemId.ToString() });
            } 
            ViewBag.alltem = dptitems;
            StaffController stf = new StaffController();
            IEnumerable<VM_Staff> allstaff = stf.GetStaffs(branchId);
            List<SelectListItem> dstfitems = new List<SelectListItem>();
            dstfitems.Add(new SelectListItem { Text = "Select a Staff", Value = "0" });
            foreach (var item in allstaff)
            {

                dstfitems.Add(new SelectListItem { Text = item.StaffName, Value = item.StaffId.ToString() });
            }
            ViewBag.IssueTo = dstfitems;
            ViewBag.BranchId = branchId;

            return View("RDItems");
        }
        public ActionResult SaveRDItems(IEnumerable<IssueRegister> items)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            StoreController _rm = new StoreController();

            bool rtnVal = _rm.AddRDItems(items);

            Session["BranchId"] = branchId;

            return RedirectToAction("IssuedItems");
        }
        public ActionResult IssuedItems(int? page, int? pSize)
        {
            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            int branchId = int.Parse(Session["BranchId"].ToString());

            StoreController str = new StoreController();
            IEnumerable<IssueRegister> ItemListModel = new List<IssueRegister>();
            ItemListModel = str.GetIssuedItems(branchId);
            IPagedList<IssueRegister> bitemlist = ItemListModel.ToPagedList(pageNumber, (int)DefaultPageSize);


            return View("IssuedItems", bitemlist);
        }
    }
}
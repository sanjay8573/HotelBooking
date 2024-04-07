using HotelBooking.Attribute;
using HotelBooking.Controllers.Restaurant;
using HotelBooking.Desig;
using HotelBooking.Model;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Repository.Interface;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using static System.Web.Razor.Parser.SyntaxConstants;


namespace HotelBooking.Controllers
{
    [CheckSessionTimeOut]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            return View();

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session["Logged_In"] = "LoggedOut";
            HttpContext.Session["UserLoginResponse"] = null;
            HttpContext.Session["CompanyId"] = null;
            HttpContext.Session["BranchId"] = null;
            HttpCookie uu = HttpContext.Request.Cookies.Get("LoginCookies");
            uu.Value = "";
            uu.Expires = DateTime.Now.AddDays(-2);
            HttpContext.Response.Cookies.Add(uu);

            //HttpContext.Response.Cookies.Remove("LoginCookies");
            Session.Abandon();
            return RedirectToAction("index", "unProHome");

        }
        public ActionResult RolesAndRights()
        {
            return View();

        }

        public ActionResult AddRolesAndRights()
        {
            int branchId;
            if (Session.Keys.Count != 0)
            {
                branchId = int.Parse(Session["BranchId"].ToString());

                ModuleController moduleController = new ModuleController();


                IEnumerable<VM_Module> VM = moduleController.GetModulesWithRights(branchId);
                Session["BranchId"] = branchId;
                return View(VM);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }
        public ActionResult ContactUs()
        {
            return View();
        }


        public ActionResult RoomType(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            IEnumerable<RoomType> RTListModel = new List<RoomType>();
            RoomTypeController RTC = new RoomTypeController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            if (pSize != null) {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            RTListModel = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false);
            Session["BranchId"] = branchId;

            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
            IPagedList<RoomType> rmlist = RTListModel.ToPagedList(pageNumber, (int)DefaultPageSize);
            List<SelectListItem> kk = new List<SelectListItem>();
            int pc = rmlist.PageCount;
            for (int i = 1; i <= pc; i++)
            {
                kk.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            ViewBag.pc = kk;

            return View(rmlist);

        }
        public ActionResult AddRoomType(int RoomTypeId = 0)
        {

            List<Amenities> amListModel = new List<Amenities>();
            AmenitiesController _am = new AmenitiesController();
            RoomTypeController _rt = new RoomTypeController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            amListModel = _am.GetAmenities(branchId).Where(i => i.isDeleted == false && i.IsActive == true).ToList();

            RoomType mrtModel = new RoomType();

            Session["BranchId"] = branchId;

            if (RoomTypeId > 0)
            {
                mrtModel = _rt.GetRoomTypes(branchId).Where(r => r.RoomTypeId == RoomTypeId).SingleOrDefault();
            }
            mrtModel.AmenitiesData = amListModel;
            return View(mrtModel);


        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveRoomType(RoomType roomTypeEntiry)
        {

            RoomTypeController RTC = new RoomTypeController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            roomTypeEntiry.BranchId = branchId;
            bool trn = RTC.AddRoomType(roomTypeEntiry);
            Session["BranchId"] = branchId;
            return RedirectToAction("RoomType");


        }

        public ActionResult DelRoomType(int RoomTypeId = 0)
        {
            if (RoomTypeId > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomTypeController _rt = new RoomTypeController();
                _rt.DeleteRoomtype(branchId, RoomTypeId);
                Session["BranchId"] = branchId;
                return RedirectToAction("RoomType");
            }

            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }

        public ActionResult GetAmenities(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
            IEnumerable<Amenities> amListModel = new List<Amenities>();
            AmenitiesController _am = new AmenitiesController();
            int branchId = int.Parse(Session["BranchId"].ToString());

            amListModel = _am.GetAmenities(branchId).Where(d => d.isDeleted == false); ;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            Session["BranchId"] = branchId;
            return View(amListModel.ToPagedList(pageNumber, (int)DefaultPageSize));


        }

        public ActionResult AddAmenities(int AmenitisId = 0)
        {

            Amenities am = new Amenities();
            int branchId = int.Parse(Session["BranchId"].ToString());


            am.BranchId = branchId;
            Session["BranchId"] = branchId;
            if (AmenitisId > 0)
            {
                AmenitiesController _am = new AmenitiesController();
                am = _am.GetAmenitieById(branchId, AmenitisId);
            }

            am.DateCreated = DateTime.Now;
            return View(am);



        }

        [HttpPost]
        public ActionResult SaveAmenities(Amenities amenitiesEntity)
        {

            HttpPostedFileBase postedFile = Request.Files["amenityImage"];
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            AmenitiesController _am = new AmenitiesController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            amenitiesEntity.BranchId = branchId;
            amenitiesEntity.imageData = bytes;
            bool trn = _am.AddAmenities(amenitiesEntity);
            Session["BranchId"] = branchId;
            return RedirectToAction("GetAmenities");


        }

        public ActionResult DelAmenity(int AmenitisId = 0)
        {
            if (AmenitisId > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                AmenitiesController _am = new AmenitiesController();
                _am.DeleteAmenities(branchId, AmenitisId);
                Session["BranchId"] = branchId;
                return RedirectToAction("GetAmenities");
            }

            else
            {
                return RedirectToAction("Error", "unProHome");
            }

        }
        public ActionResult RoomTypeImage(int RoomTypeId, int? page, int? pSize)
        {
            int? DefaultPageSize = 10;

            RoomTypeImageController _RTI = new RoomTypeImageController();
            ViewBag.RoomTypeId = RoomTypeId;
            IEnumerable<RoomeTypeImages> RTIS = _RTI.GetRoomTypeImages(RoomTypeId).Where(d => d.isActive == true && d.isDeleted == false);
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
            return View(RTIS.ToPagedList(pageNumber, (int)DefaultPageSize));

        }
        [HttpPost]
        public ActionResult AddRoomTypeImage(FormCollection form)
        {


            HttpPostedFileBase postedFile = Request.Files["RTImageData"];
            RoomTypeImageController _RTI = new RoomTypeImageController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            int RoomTyepId = int.Parse(form["RoomTypeId"].ToString());
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            FileViewModel FVM = new FileViewModel();
            ReturnData RD;
            FVM.ImageName = postedFile.FileName;
            FVM.ImageData = bytes;
            FVM.RoomTyepId = RoomTyepId;
            FVM.BranchId = branchId;

            Session["BranchId"] = branchId;
            RD = _RTI.SaveRoomTypeImage(FVM);

            return RedirectToAction("RoomTypeImage", "Home", new { RoomTypeId = RoomTyepId });


        }

        public ActionResult Floor(int FloorId = 0)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());

            Floor fl = new Floor();
            fl.BranchId = branchId;
            FloorController fc = new FloorController();
            if (FloorId > 0)
            {
                fl = fc.GetFloors(branchId).Where(f => f.FloorId == FloorId).SingleOrDefault();
            }
            Session["BranchId"] = branchId;
            return View("MangeFloor", fl);

        }
        [HttpPost]
        public ActionResult SaveFloor(Floor floorEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            FloorController _fl = new FloorController();
            bool rtn = _fl.AddFloor(floorEntity);
            Session["BranchId"] = branchId;
            return RedirectToAction("FloorDetails");

        }


        public ActionResult DelFloor(int FloorId)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            FloorController _fl = new FloorController();
            _fl.DelFloor(FloorId);
            Session["BranchId"] = branchId;
            return RedirectToAction("FloorDetails");

        }

        public ActionResult FloorDetails(int? page, int? pSize)
        {
            int? DefaultPageSize = 10;


            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }
            ViewBag.pSize = new List<SelectListItem>()
                    {

                        new SelectListItem() { Value="2", Text= "2" ,Selected=(2==DefaultPageSize) },
                        new SelectListItem() { Value="5", Text= "5" ,Selected=(5==DefaultPageSize)  },
                        new SelectListItem() { Value="10", Text= "10" ,Selected=(10==DefaultPageSize) },
                        new SelectListItem() { Value="15", Text= "15" ,Selected=(15==DefaultPageSize) },
                        new SelectListItem() { Value="20", Text= "20" ,Selected=(25==DefaultPageSize) },
                     };
            int branchId = int.Parse(Session["BranchId"].ToString());
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            FloorController _fl = new FloorController();
            IEnumerable<Floor> flrs = _fl.GetFloors(branchId).Where(d => d.isDeleted == false);
            Session["BranchId"] = branchId;
            return View("FloorDetails", flrs.ToPagedList(pageNumber, (int)DefaultPageSize));


        }

        public ActionResult EditFloor(int FloorId)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            ViewBag.branchId = branchId;
            FloorController fc = new FloorController();

            Floor fl = fc.GetFloors(branchId).Where(f => f.FloorId == FloorId).SingleOrDefault();
            Session["BranchId"] = branchId;
            return View("MangeFloor", fl);

        }

        public ActionResult Room(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();
            IEnumerable<Room> rmModel = _rm.GetRooms(branchId).Where(d => d.isDeleted == false);

            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };



            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            //List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            //RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            //foreach (RoomType item in rtm)
            //{
            //    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title });
            //}
            //ViewBag.RTComboModel = RoomTypeitems;
            ViewBag.RoomNumber = "";
            Session["BranchId"] = branchId;
            return View(rmModel.ToPagedList(pageNumber, (int)DefaultPageSize));

        }

        public ActionResult AddRoom()
        {


            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();
            Room rmModel = new Room();
            rmModel.BranchId = branchId;
            FloorController _fl = new FloorController();
            IEnumerable<Floor> flrs = _fl.GetFloors(branchId).Where(d => d.isDeleted == false && d.isActive == true);


            List<SelectListItem> floortems = new List<SelectListItem>();
            floortems.Add(new SelectListItem { Text = "Select a Floor", Value = "0" });
            foreach (Floor item in flrs)
            {
                floortems.Add(new SelectListItem { Text = item.FloorNumber + "-" + item.Description, Value = item.FloorId.ToString() });
            }
            ViewBag.FloorComboModel = floortems;

            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true);




            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            ViewBag.RoomNumber = "";
            Session["BranchId"] = branchId;
            return View(rmModel);

        }
        public ActionResult EditRoom(int Roomid)
        {


            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();
            Room rmModel = _rm.GetRooms(branchId).Where(r => r.RoomId == Roomid).SingleOrDefault();

            rmModel.BranchId = branchId;
            FloorController _fl = new FloorController();
            IEnumerable<Floor> flrs = _fl.GetFloors(branchId).Where(d => d.isDeleted == false && d.isActive == true);


            List<SelectListItem> floortems = new List<SelectListItem>();
            floortems.Add(new SelectListItem { Text = "Select a Floor", Value = "0" });
            foreach (Floor item in flrs)
            {
                bool selected = item.FloorId == rmModel.floor;
                floortems.Add(new SelectListItem { Text = item.FloorNumber + "-" + item.Description, Value = item.FloorId.ToString(), Selected = selected });
            }
            ViewBag.FloorComboModel = floortems;

            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true);




            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                bool selected = item.RoomTypeId == rmModel.RoomTypeId;
                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title, Selected = selected });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            ViewBag.RoomNumber = "";
            Session["BranchId"] = branchId;
            return View(rmModel);

        }
        [HttpPost]
        public ActionResult SaveRoom(IEnumerable<Room> roomEntities)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();
            roomEntities.ForEach(item =>
            {
                bool rtnVal = _rm.AddRoom(item);
            }

            );
            Session["BranchId"] = branchId;
            return Json("Success", JsonRequestBehavior.AllowGet);
            // return RedirectToAction("Room");
        }
        public ActionResult SaveEditedRoom(Room roomEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();

            bool rtnVal = _rm.AddRoom(roomEntity);

            Session["BranchId"] = branchId;

            return RedirectToAction("Room");
        }
        public ActionResult DelRoom(int RoomId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();

            _rm.DeleteRoom(RoomId);
            Session["BranchId"] = branchId;
            return RedirectToAction("Room");
        }

        public ActionResult PaidServices(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            int branchId = int.Parse(Session["BranchId"].ToString());
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            PaidServicesController _psc = new PaidServicesController();
            IEnumerable<PaidServices> rtm = _psc.GetpadServices(branchId).Where(d => d.isDeleted == false);
            Session["BranchId"] = branchId;
            return View(rtm.ToPagedList(pageNumber, (int)DefaultPageSize));

        }


        public ActionResult AddPaidServices(int PaidServiceId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            PaidServices ps = new PaidServices();
            if (PaidServiceId > 0)
            {
                PaidServicesController _ps = new PaidServicesController();
                ps = _ps.GetpadServices(branchId).Where(p => p.PaidServiceId == PaidServiceId).SingleOrDefault();
            }
            else
            {
                ps.BranchId = branchId;
            }


            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;
            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                bool slt = false;
                if (PaidServiceId > 0)
                {
                    if (ps.RoomTypeId.Contains(item.RoomTypeId.ToString()))
                    {
                        slt = true;
                    }
                }

                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString(), Selected = slt });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            List<PriceType> PTlist = new List<PriceType>()
            {
                new PriceType()
                {
                     BranchId = branchId,
                     PriceTypeId=1,
                     PriceTypeTitle= "Fix Price",
                },
                 new PriceType()
                {
                     BranchId = branchId,
                     PriceTypeId=2,
                     PriceTypeTitle= "Price Per Persom",
                },
                  new PriceType()
                {
                     BranchId = branchId,
                     PriceTypeId=3,
                     PriceTypeTitle= "Per Night",
                }
            };
            List<SelectListItem> PriceTypeitems = new List<SelectListItem>();
            PriceTypeitems.Add(new SelectListItem { Text = "Select a Price Type", Value = "0" });
            foreach (PriceType item in PTlist)
            {
                bool slt = false;
                if (PaidServiceId > 0)
                {
                    if (ps.PriceTypeId == item.PriceTypeId)
                    {
                        slt = true;
                    }
                }
                PriceTypeitems.Add(new SelectListItem { Text = item.PriceTypeTitle, Value = item.PriceTypeId.ToString(), Selected = slt });
            }
            ViewBag.PriceTypeModel = PriceTypeitems;
            Session["BranchId"] = branchId;
            return View(ps);
        }
        public ActionResult DelPaidServices(int PaidServiceId = 0)
        {

            PaidServicesController _ps = new PaidServicesController();
            _ps.DeletePaidServices(PaidServiceId);
            return RedirectToAction("PaidServices");

        }


        [HttpPost]
        public ActionResult SavePaidServices(PaidServices paidServiceEntity)
        {

            PaidServicesController _psc = new PaidServicesController();
            bool rtnVal = _psc.AddPaidServices(paidServiceEntity);
            return RedirectToAction("PaidServices");
        }



        public ActionResult PriceManager(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }
            int branchId = int.Parse(Session["BranchId"].ToString());
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            pmController _psc = new pmController();
            IEnumerable<PriceManager> pcm = _psc.GetAllPrice(branchId).Where(d => d.isDeleted == false);
            Session["BranchId"] = branchId;
            return View(pcm.ToPagedList(pageNumber, (int)DefaultPageSize));

        }

        public ActionResult AddPrice(int PriceManagerId = 0, int tab = 1)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomTypeController RTC = new RoomTypeController();
            PriceManager PM = new PriceManager();
            PM.BranchId = branchId;
            if (PriceManagerId > 0)
            {
                pmController _pm = new pmController();
                PM = _pm.GetAllPrice(branchId).Where(p => p.PriceManageId == PriceManagerId).SingleOrDefault();
            }
            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;
            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                bool slt = false;
                if (PriceManagerId > 0)
                {
                    if (PM.RoomTypeId == item.RoomTypeId)
                    {
                        slt = true;
                    }
                }
                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString(), Selected = slt });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            ViewBag.SelectedTab = tab;
            ViewBag.PriceManageId = PM.PriceManageId;
            Session["BranchId"] = branchId;
            return View();

        }


        public PartialViewResult _RegularPrice(int PriceManageId = 0)
        {


            int branchId = int.Parse(Session["BranchId"].ToString());

            PriceManager ps = new PriceManager();
            if (PriceManageId > 0)
            {
                pmController _pm = new pmController();
                ps = _pm.GetAllPrice(branchId).Where(p => p.PriceManageId == PriceManageId).SingleOrDefault();
            }
            ps.BranchId = branchId;
            Session["BranchId"] = branchId;
            return PartialView("_RegularPrice", ps);

        }
        public ActionResult DelPrice(int PriceManageId)
        {
            pmController _ps = new pmController();
            _ps.DeletePrice(PriceManageId);
            return RedirectToAction("PriceManager");

        }

        public PartialViewResult _SpecialPrice(int PriceManageId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());



            SPM ps = new SPM();
            if (PriceManageId > 0)
            {
                SPMController _pm = new SPMController();
                ps = _pm.GetAllSpecialPrice(branchId).Where(p => p.SpecialPriceManageId == PriceManageId).SingleOrDefault();
            }
            Session["BranchId"] = branchId;
            ps.BranchId1 = branchId;
            return PartialView("_SpecialPrice", ps);
        }

        [HttpPost]
        public ActionResult SaveRegularPrice(PriceManager pmEntity)
        {

            pmController pmc = new pmController();
            bool rtnVal = pmc.AddPrice(pmEntity);
            return RedirectToAction("PriceManager");
        }
        [HttpPost]
        public ActionResult SaveSpecialPrice(SPM pmEntity)
        {

            SPMController pmc = new SPMController();
            bool rtnVal = pmc.AddSpecialPrice(pmEntity);
            return RedirectToAction("PriceManager");
        }

        public PartialViewResult _SpecialPriceGrid(int? page, int? pSize)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }
            SPMController _pm = new SPMController();

            IEnumerable<SPM> ps = _pm.GetAllSpecialPrice(branchId).Where(d => d.isDeleted == false);

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
            Session["BranchId"] = branchId;
            return PartialView("_SpecialPriceGrid", ps.ToPagedList(pageNumber, (int)DefaultPageSize));
        }

        public ActionResult Coupon(int? page, int? pSize)
        {
            int? DefaultPageSize = 10;


            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
            int branchId = int.Parse(Session["BranchId"].ToString());
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            CouponController _cp = new CouponController();
            IEnumerable<Coupon> cpModel = _cp.GetCoupons(branchId);
            Session["BranchId"] = branchId;
            return View("Coupons", cpModel.ToPagedList(pageNumber, (int)DefaultPageSize));


        }
        public ActionResult AddCoupon(int couponId = 0)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            //Roomtypes
            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;


            ViewBag.RTModel = rtm;
            //PaidService
            PaidServicesController psd = new PaidServicesController();
            IEnumerable<PaidServices> ps = new List<PaidServices>();
            ps = psd.GetpadServices(branchId);

            ViewBag.PSModel = ps;
            //users
            StaffController stf = new StaffController();

            IEnumerable<StaffTier> stm = new List<StaffTier>();
            stm = stf.GetStaffTier(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;
            ViewBag.StaffModel = stm;
            Session["BranchId"] = branchId;
            if (couponId > 0)
            {
                CouponController pmc = new CouponController();
                Coupon cModel = pmc.GetCoupon(branchId, couponId);
                return View(cModel);

            }
            else
            {
                Coupon cModel1 = new Coupon();

                cModel1.BranchId = branchId;
                return View(cModel1);
            }



        }

        [HttpPost]
        public ActionResult SaveCoupon(Coupon cpEntity)
        {
            HttpPostedFileBase postedFile = Request.Files["cpImage"];
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            int branchId = int.Parse(Session["BranchId"].ToString());
            cpEntity.BranchId = branchId;
            cpEntity.ImageData = bytes;
            CouponController pmc = new CouponController();
            bool rtnVal = pmc.SaveCoupon(cpEntity);
            Session["BranchId"] = branchId;
            return RedirectToAction("Coupon");
        }

        public ActionResult DelCoupon(int couponId = 0)
        {
            if (couponId > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                CouponController _cp = new CouponController();
                _cp.DeleteCoupon(branchId, couponId);
                Session["BranchId"] = branchId;
                return RedirectToAction("Coupon");
            }

            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }



        public ActionResult Bookings(int? page, int? pSize)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true);




            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            int? DefaultPageSize = 10;
            IEnumerable<Booking> bksListModel = new List<Booking>();
            BookingController _bks = new BookingController();

            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            var dateCriteria = DateTime.Now.Date.AddDays(-7);// last 7 days booking


            bksListModel = _bks.GetBookings(branchId);//.Where(d => d.isDeleted == false);
            var QueryDeatils = from tmpBooking in bksListModel
                               orderby tmpBooking.BookingDate descending
                               where tmpBooking.BookingDate >= dateCriteria

                               select tmpBooking;

            /* var QueryDeatils1 = from tmpBooking1 in bksListModel
                                 orderby DateTime.Parse(tmpBooking1.CheckIn) ascending
                                 where DateTime.Parse(tmpBooking1.CheckIn) >= dateCriteria

                                 select tmpBooking1;*/

            Session["BranchId"] = branchId;

            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
            IPagedList<Booking> bkslist = QueryDeatils.ToPagedList(pageNumber, (int)DefaultPageSize);



            return View(bkslist);

        }
        public ActionResult AddBooking(int bookingId = 0)
        {


            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomTypeController RTC = new RoomTypeController();
            GuestsController _gs = new GuestsController();
            BookingController _bk = new BookingController();
            Booking bk = new Booking();

            if (bookingId > 0)
            {
                bk = _bk.GetBooking(bookingId);

            }
            bk.BranchId = branchId;
            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;
            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                bool slt = false;
                if (bookingId > 0)
                {
                    if (item.RoomTypeId.ToString() == bk.RoomTypeId)
                        slt = true;
                }
                RoomTypeitems.Add(new SelectListItem { Text = item.Title.Trim(), Value = item.RoomTypeId.ToString(), Selected = slt });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            if (bookingId > 0)
            {
                PaidServicesController _ps = new PaidServicesController();
                IEnumerable<PaidServices> ps = _ps.GetPaidServicesByRoomType(1).Where(i => i.isActive == true && i.BranchId == branchId && i.isDeleted == false);

                ViewBag.PS = ps;

            }
            IEnumerable<Guests> gst = new List<Guests>();
            gst = _gs.GetAllGuests(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;
            List<SelectListItem> AllGuest = new List<SelectListItem>();
            AllGuest.Add(new SelectListItem { Text = "Select a Guest", Value = "0" });
            foreach (Guests item in gst)
            {
                bool slt = false;
                if (bookingId > 0)
                {
                    if (item.GuestId == bk.GuestId)
                        slt = true;
                }
                AllGuest.Add(new SelectListItem { Text = item.Name.Trim(), Value = item.GuestId.ToString(), Selected = slt });
            }
            ViewBag.GSComboModel = AllGuest;

            Session["BranchId"] = branchId;
            return View("CreateBooking", bk);
            //return View(bk);

        }
        public bool SaveBooking(BookingRequest bookingEntity)
        {
            BookingController _bk = new BookingController();
            return _bk.AddBooking(bookingEntity);
        }
        public enum BookingStatus
        {
            Select = 0,
            New = 1,
            Pending = 2,
            Rejected = 3,
            Success = 4
        }
        public enum PaymentStatus
        {
            Select = 0,
            Waiting = 1,
            Pending = 2,
            Confirm = 3,
            Success = 4
        }


        public ActionResult BookingProcess(int BookingId, string BookingRef)
        {
            try
            {
                ViewBag.BookingId = BookingId;
                ViewBag.BookingRef = BookingRef;
                int branchId = int.Parse(Session["BranchId"].ToString());
                ViewBag.BranchId = branchId;
                IEnumerable<BookingStatus> bkStatus = Enum.GetValues(typeof(BookingStatus))
                                              .Cast<BookingStatus>();
                IEnumerable<PaymentStatus> pmtStatus = Enum.GetValues(typeof(PaymentStatus))
                                             .Cast<PaymentStatus>();
                BookingController _bc = new BookingController();
                VM_BookingDetails VMB = _bc.GetBookingDetails(branchId, BookingId);

                ViewBag.bkStatus = from bk in bkStatus
                                   select new SelectListItem
                                   {
                                       Text = bk.ToString(),
                                       Value = bk.ToString(),
                                       Selected = (bk.ToString().ToUpper() == VMB.BookingStatus.ToUpper())
                                   };

                ViewBag.pmtStatus = from bk in pmtStatus
                                    select new SelectListItem
                                    {
                                        Text = bk.ToString(),
                                        Value = bk.ToString(),
                                        Selected = (bk.ToString().ToUpper() == VMB.PaymnentStatus.ToUpper())
                                    };
                Session["BranchId"] = branchId;
                return View(VMB);
            }
            catch (Exception)
            {

                return View("LogOff");
            }


        }


        public PartialViewResult _BookingDetails(int BookingId, string RoomTypeId, string cinDate, string coutDate)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            BookingController _bc = new BookingController();
            IEnumerable<BookingCost> _pr = _bc.GetBookingsCost(BookingId);
            Session["BranchId"] = branchId;
            return PartialView("_BookingDetails", _pr);

        }
        public PartialViewResult _Payments(int BranchId, int BookingId, int? page, int? pSize)
        {
            IPagedList<BookingPayments> bkplist = null;
            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                int? DefaultPageSize = 10;
                BookingController _bc = new BookingController();
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;
                IEnumerable<BookingPayments> _BP = _bc.GetBookingPayments(BranchId, BookingId);
                ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
                decimal PaidAmount = _BP.Select(t => t.paymentAmount).Sum();
                ViewBag.PaidAmount = PaidAmount;
                bkplist = _BP.ToPagedList(pageNumber, (int)DefaultPageSize);

            }

            return PartialView("_Payments", bkplist);
        }
        public PartialViewResult _AllocateRooms(int BookingId, string RoomTypeId, int nor)
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rc = new RoomController();

            //IEnumerable<Room> rm = _rc.GetRoomsByRoomTypeIds(branchId, RoomTypeId, BookingId);
            AllocateRoomResponse rm = _rc.GetRoomsByRoomTypeIds(branchId, RoomTypeId, BookingId);
            List<SelectListItem> Roomitems = new List<SelectListItem>();
            Roomitems.Add(new SelectListItem { Text = "Select a Room", Value = "0" });
            foreach (Room item in rm.AvailableRooms)
            {
                Roomitems.Add(new SelectListItem { Text = item.RoomTypeName + "-" + item.RoomNumber + "-" + item.FloorName, Value = item.RoomId.ToString() + "-" + item.floor.ToString() });
            }
            ViewBag.RMComboModel = Roomitems;
            // ViewBag.AllocatedRoomNotCheckOut=
            ViewBag.AllocatedRoomModel = rm.BookedRooms.Where(c => c.isCheckout == false).ToArray();
            int alreadyallocated = rm.BookedRooms.Count();
            ViewBag.nRooms = nor - alreadyallocated;
            Session["BranchId"] = branchId;


            return PartialView("_AllocateRooms");

        }

        public PartialViewResult _Documents(int BookingId, int noPax, int noRooms, int? page, int? pSize)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }
            int pageNumber = page ?? 1;

            DocumentController DC = new DocumentController();
            IEnumerable<DocumentType> DocTypeList = DC.GetDocumentTypes(branchId);

            List<SelectListItem> avlDocs = new List<SelectListItem>();
            avlDocs.Add(new SelectListItem { Text = "Select Document Type", Value = "0", Selected = true });
            foreach (DocumentType item in DocTypeList)
            {
                bool slt = false;

                avlDocs.Add(new SelectListItem { Text = item.DocumentTypeName.Trim(), Value = item.DocumentTypeId.ToString(), Selected = slt });
            }
            ViewBag.DocsComboModel = avlDocs;

            VM_DocumentResponse vM_DocumentResponse = new VM_DocumentResponse();
            vM_DocumentResponse.BookingId = BookingId;
            vM_DocumentResponse.Pax = noPax;
            vM_DocumentResponse.Rooms = noRooms;
            vM_DocumentResponse.Documents = DC.GetDocuments(BookingId).ToPagedList(pageNumber, (int)DefaultPageSize); ;

            return PartialView("_Documents", vM_DocumentResponse);

        }

        public bool SaveAllocatedRoom(BookedRoom bkdEntity)
        {
            BookingController _bk = new BookingController();
            return _bk.AllocateRoom(bkdEntity);
        }

        public bool Addpayments(BookingPayments bkpEntity)
        {
            BookingController _bk = new BookingController();
            return _bk.AddBookingPayments(bkpEntity);
        }

        public bool UpdateBookingStatus(int BookingId, string status)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            BookingController _bk = new BookingController();
            return _bk.updateBookingStatus(branchId, BookingId, status);
        }

        public bool UpdatePaymentStatus(int BookingId, string status)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            BookingController _bk = new BookingController();
            return _bk.updatePaymentStatus(branchId, BookingId, status);
        }


        public PartialViewResult _PricePerRoom(int nOfr, int todalNight)
        {



            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomTypeController RTC = new RoomTypeController();
            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId).Where(d => d.isDeleted == false && d.isActive == true); ;
            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                bool slt = false;
                //if (bookingId > 0)
                //{
                //    if (item.RoomTypeId == bk.RoomTypeId)
                //        slt = true;
                //}
                RoomTypeitems.Add(new SelectListItem { Text = item.Title.Trim(), Value = item.RoomTypeId.ToString(), Selected = slt });
            }
            ViewBag.RTComboModel1 = RoomTypeitems;

            ViewBag.NOR = nOfr;
            ViewBag.todalNight = todalNight;

            return PartialView("_PricePerRoom");

        }
        public PartialViewResult _PricePerNight(string RoomTypeId, string cinDate, string coutDate, string refDiv)
        {

            PriceRequest pr = new PriceRequest();
            pr.CheckInDate = cinDate;
            pr.CheckOutDate = coutDate;
            pr.roomTypeId = int.Parse(RoomTypeId);
            pr.nOfRoom = int.Parse(refDiv);
            int branchId = int.Parse(Session["BranchId"].ToString());

            BookingController _bc = new BookingController();
            IEnumerable<PriceResponse> _pr = _bc.GetPricesForNight(pr);


            return PartialView("_PricePerNightNew", _pr);

        }
        public PartialViewResult _PaidServices(string RoomTypeId)
        {

            int roomTypeId = int.Parse(RoomTypeId);
            int branchId = int.Parse(Session["BranchId"].ToString());

            PaidServicesController _ps = new PaidServicesController();
            IEnumerable<PaidServices> ps = _ps.GetPaidServicesByRoomType(roomTypeId).Where(i => i.isActive == true && i.BranchId == branchId && i.isDeleted == false);


            return PartialView("_PaidServices", ps);

        }


        [HttpPost]
        public decimal ApplyCoupon(int BranchId, string couponCode)
        {
            CouponController CC = new CouponController();
            CouponResponse cr = CC.ApplyCoupon(couponCode);

            return cr.CouponValue;
        }


        public ActionResult BookedRoom(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            int branchId = int.Parse(Session["BranchId"].ToString());

            BookedRoomController _bm = new BookedRoomController();
            IEnumerable<BookedRoom> VBR = _bm.GetAllBookedRoom(branchId);

            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            var rst = (from item in VBR
                       select new
                       {
                           item.RoomTypeId,
                           item.RoomTypeName
                       }).Distinct();
            RoomTypeitems.Add(new SelectListItem { Text = "Filter by  Room Type", Value = "0" });
            foreach (var item in rst)
            {
                RoomTypeitems.Add(new SelectListItem { Text = item.RoomTypeName, Value = item.RoomTypeId.ToString() });
            }
            ViewBag.RTComboModel = RoomTypeitems;
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" ,Selected=(2==pSize)},
                        new SelectListItem() { Value="5", Text= "5",Selected=(5==pSize) },
                        new SelectListItem() { Value="10", Text= "10" ,Selected=(10==pSize)},
                        new SelectListItem() { Value="15", Text= "15",Selected=(15==pSize)},
                        new SelectListItem() { Value="20", Text= "20" ,Selected=(20==pSize)},
                     };

            IPagedList<BookedRoom> bkslist = VBR.ToPagedList(pageNumber, (int)DefaultPageSize);
            return View(bkslist);

        }


        public ActionResult Checkout(int bookingId = 0)
        {


            int branchId = int.Parse(Session["BranchId"].ToString());
            ViewBag.BranchId = branchId;
            BookingController _bc = new BookingController();
            VM_BookingDetails VMB = _bc.GetBookingDetails(branchId, bookingId);

            return View("CheckOut", VMB);

        }

        public ActionResult Guests(int? page, int? pSize)
        {

            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }

            int pageNumber = page ?? 1;
            ViewBag.PageSize = DefaultPageSize;
            int branchId = int.Parse(Session["BranchId"].ToString());

            GuestsController _bm = new GuestsController();
            IEnumerable<Guests> GS = _bm.GetAllGuests(branchId);

            ViewBag.NumberOfVIP = GS.Where(v => v.isVIP == true).Count();
            ViewBag.NumberOfGuest = GS.Count();


            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            IPagedList<Guests> gslist = GS.ToPagedList(pageNumber, (int)DefaultPageSize);
            return View(gslist);

        }
        public ActionResult AddGuest(int GuestId = 0)
        {

            //bookingId = 2;
            int branchId = int.Parse(Session["BranchId"].ToString());

            GuestsController _pg = new GuestsController();
            Guests bk = new Guests();

            if (GuestId > 0)
            {
                bk = _pg.GetGuest(branchId, GuestId);

            }
            bk.BranchId = branchId;

            ViewBag.StaticCountry = new List<SelectListItem>()
                    {
                       new SelectListItem() { Value="0", Text= "Select Country"  },
                        new SelectListItem() { Value="India", Text= "India", Selected=(bk.country=="India") },
                        new SelectListItem() { Value="USA", Text= "USA", Selected=(bk.country=="USA") },
                        new SelectListItem() { Value="UK", Text= "UK" , Selected=(bk.country=="UK")},
                        new SelectListItem() { Value="Canada", Text= "Canada" , Selected=(bk.country=="Canada")},
                        new SelectListItem() { Value="France", Text= "France", Selected=(bk.country=="France") },
                     };




            return View(bk);

        }
        public ActionResult SaveGuest(Guests guestEntity)
        {


            int branchId = int.Parse(Session["BranchId"].ToString());
            guestEntity.BranchId = branchId;
            GuestsController _bk = new GuestsController();
            bool rtn = _bk.AddGuest(guestEntity);
            return RedirectToAction("Guests");

        }
        public ActionResult MakeVIP(int GuestId)
        {

            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());

                GuestsController _bk = new GuestsController();
                _bk.SetAsVip(branchId, GuestId);
                return RedirectToAction("Guests");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult DelGuest(int GuestId)
        {


            int branchId = int.Parse(Session["BranchId"].ToString());

            GuestsController _bk = new GuestsController();
            _bk.DeleteGuest(branchId, GuestId);
            return RedirectToAction("Guests");

        }
        public ActionResult SaveBookingDocuments()
        {

            int branchId = int.Parse(Session["BranchId"].ToString());
            DocumentController _dc = new DocumentController();
            int BookingId = int.Parse(Request.Form["BookingId"].ToString());
            string BookingRef = Request.Form["BookingRef1"].ToString();
            bool isRCRequired = bool.Parse(Request.Form["isRCRequired"].ToString());
            if (Request.Files.Count > 0)
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {

                    if (isRCRequired)
                    {
                        if (Request.Files.Keys[i].Contains("RC"))
                        {
                            string DocTypeName1 = Request.Form["PaxIdRC"].ToString();
                            HttpPostedFileBase postedFile1 = Request.Files["FileRC"];
                            byte[] bytes1;
                            using (BinaryReader br = new BinaryReader(postedFile1.InputStream))
                            {
                                bytes1 = br.ReadBytes(postedFile1.ContentLength);
                            }
                            BookingDocuments bdRC = new BookingDocuments();
                            bdRC.DocumentName = "Vechele Registration" + DocTypeName1;
                            bdRC.DocumentData = bytes1;
                            bdRC.BookingId = BookingId;
                            bdRC.DocumentTypeId = 4;
                            bdRC.PaxName = DocTypeName1;
                            bdRC.isActive = true;
                            _dc.AddDocuments(bdRC);
                        }

                    }

                    if (Request.Files.Keys[i].Contains("Doc"))
                    {
                        string _DocTypeid = "DocSelType" + i.ToString();
                        string _DocTypeName = "DocTypeName" + i.ToString();
                        string _PaxId = "Pax" + i.ToString();

                        string DocTypeId = Request.Form[_DocTypeid].ToString();
                        string DocTypeName = Request.Form[_DocTypeName].ToString();
                        string PaxName = Request.Form[_PaxId].ToString();
                        string fileName = "DocFile" + i.ToString();
                        HttpPostedFileBase postedFile = Request.Files[fileName];
                        byte[] bytes;


                        using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                        {
                            bytes = br.ReadBytes(postedFile.ContentLength);
                        }
                        if (bytes.Length > 0)
                        {
                            BookingDocuments bd = new BookingDocuments();
                            bd.DocumentName = DocTypeName;
                            bd.DocumentData = bytes;
                            bd.BookingId = BookingId;
                            bd.DocumentTypeId = int.Parse(DocTypeId);
                            bd.PaxName = PaxName;
                            bd.isActive = true;
                            _dc.AddDocuments(bd);
                        }
                    }




                }

            }

            Session["BranchId"] = branchId;
            return RedirectToAction("BookingProcess", new { BookingId = BookingId, BookingRef = BookingRef });


        }

        [HttpGet]
        public JsonResult GetNoOfBookings()
        {
            DashBoardData ds = new DashBoardData();

            try
            {

                int branchId = int.Parse(Session["BranchId"].ToString());

                IEnumerable<Booking> bksListModel = new List<Booking>();
                BookingController _bks = new BookingController();
                ds = _bks.DashboardData(branchId);//.Where(d => d.isDeleted == false);

            }
            catch (Exception)
            {

                return Json("Error");
            }


            return Json(ds, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public bool CheckOut(int BookingId, int RoomId)
        {
            bool rtnVal = false;
            try
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                CheckOutRequest CR = new CheckOutRequest();
                CR.BranchId = branchId;
                CR.BookedRoomId = RoomId;
                CR.BookingId = BookingId;
                BookedRoomController BRC = new BookedRoomController();
                BRC.checkOut(CR);
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }


            return rtnVal;
        }

        public PartialViewResult _RoomCheckout(int BookingId, int noRooms, int? page, int? pSize)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            int? DefaultPageSize = 10;
            if (pSize != null)
            {
                DefaultPageSize = pSize;
            }
            int pageNumber = page ?? 1;

            BookedRoomController DC = new BookedRoomController();
            IEnumerable<BookedRoom> BR = DC.GetAllBookedRoomByBookingId(branchId, BookingId);
            IEnumerable<BookedRoom> OccupiedRoom = BR.Where(c => c.isCheckout == false).ToArray();
            IEnumerable<BookedRoom> ReleasedRoom = BR.Where(c => c.isCheckout == true).ToArray();

            VM_BookedRoomResponse vM_BRResponse = new VM_BookedRoomResponse();
            vM_BRResponse.BookingId = BookingId;
            //vM_BRResponse.Pax = noPax;
            vM_BRResponse.Rooms = noRooms;
            vM_BRResponse.BookedRooms = OccupiedRoom.ToPagedList(pageNumber, (int)DefaultPageSize);
            vM_BRResponse.ReleasedRoom = ReleasedRoom.ToPagedList(pageNumber, (int)DefaultPageSize);

            return PartialView("_RoomCheckout", vM_BRResponse);

        }


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
                ViewBag.PageSize = DefaultPageSize;
                ViewBag.pSize = new List<SelectListItem>()
                    {
                       new SelectListItem() { Value="2", Text= "2"},
                        new SelectListItem() { Value="5", Text= "5"},
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15"},
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
        public ActionResult AddEmployees(int EmpId = 0)
        {
            StaffController _stf = new StaffController();
            Staff stf = new Staff();
            int dptId = 0;
            int teamId = 0;
            int desigId = 0;
            int roleid = 0;

            if (EmpId > 0)
            {
                stf = _stf.GetStaff(EmpId);
                dptId = stf.DepartmentId;
                teamId = stf.TeamId;
                desigId = stf.DesignationId;
                roleid = stf.PrimaryRoleID;

            }
            int branchId = int.Parse(Session["BranchId"].ToString());

            DeptController _dpt = new DeptController();
            DesigController _dsg = new DesigController();
            TeamController _tm = new TeamController();
            RolesController _roles = new RolesController();


            IEnumerable<Dept> dpt = _dpt.GetDeptDetails(branchId);
            IEnumerable<Designation> dsg = _dsg.GetAllDesignation(branchId);
            IEnumerable<Team> tm = _tm.GetTeams(branchId);
            IEnumerable<RoleMaster> rl = _roles.GetRoles(branchId);

            List<SelectListItem> dptitems = new List<SelectListItem>();
            dptitems.Add(new SelectListItem { Text = "Select a Department", Value = "0" });
            foreach (var item in dpt)
            {
                bool seleted = (dptId == item.Id);
                dptitems.Add(new SelectListItem { Text = item.DepartmentName, Value = item.Id.ToString(), Selected = seleted });
            }

            ViewBag.DEPTComboModel = dptitems;

            List<SelectListItem> dsgitems = new List<SelectListItem>();
            dsgitems.Add(new SelectListItem { Text = "Select a Designation", Value = "0" });
            foreach (var item in dsg)
            {
                bool seleted = (desigId == item.Id);
                dsgitems.Add(new SelectListItem { Text = item.DesignationName, Value = item.Id.ToString(), Selected = seleted });
            }

            ViewBag.DesigComboModel = dsgitems;

            List<SelectListItem> tmitems = new List<SelectListItem>();
            tmitems.Add(new SelectListItem { Text = "Select a team", Value = "0" });
            foreach (var item in tm)
            {
                bool seleted = (teamId == item.Id);
                tmitems.Add(new SelectListItem { Text = item.TeamName, Value = item.Id.ToString(), Selected = seleted });
            }


            ViewBag.TeamComboModel = tmitems;

            List<SelectListItem> rlitems = new List<SelectListItem>();
            rlitems.Add(new SelectListItem { Text = "Select Primary  Role", Value = "0" });
            foreach (var item in rl)
            { bool seleted = (roleid == item.Id);
                rlitems.Add(new SelectListItem { Text = item.RoleName, Value = item.Id.ToString(), Selected = seleted });
            }


            ViewBag.RoleComboModel = rlitems;




            return View(stf);
        }


        public ActionResult SaveEmployee(Staff staffEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            StaffController _stf = new StaffController();
            staffEntity.BranchId = branchId;


            _stf.AddStaff(staffEntity);

            return RedirectToAction("Employees", "Home");
        }
        public ActionResult LoginSetup(int staffId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            StaffController _stf = new StaffController();
            StaffLogin stfl = new StaffLogin();

            if (staffId > 0)
            {

                stfl = _stf.LoginSetup(staffId);
            }
            return View("LoginSetup", stfl);

        }
        public ActionResult SetupLogin(StaffLogin staffloginEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            StaffController _stf = new StaffController();
            int rtnVal = _stf.StaffLoginSetup(staffloginEntity);


            return RedirectToAction("Employees", "Home");

        }


        public ActionResult AddDept(int DeptId = 0)
        {
            DeptController _stf = new DeptController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            Dept dpt = new Dept();
            dpt.BranchId = branchId;
            if (DeptId > 0)
            {
                dpt = _stf.GetDeptById(DeptId);
            }

            return View("AddDept", dpt);
        }
        public ActionResult SaveDept(Dept DeptEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DeptController _stf = new DeptController();
            _stf.AddDeptDetails(DeptEntity, branchId);

            return RedirectToAction("Dept", "Home");
        }

        public ActionResult DelDept(int DeptId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DeptController _stf = new DeptController();
            _stf.DeleteDepartment(DeptId);

            return RedirectToAction("Dept", "Home");
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

            return RedirectToAction("Designation", "Home");
        }
        public ActionResult DelDesig(int DesigId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            DesigController _stf = new DesigController();
            _stf.DelDesignation(DesigId);

            return RedirectToAction("Dept", "Home");
        }
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


        public PartialViewResult _BookingInvoive(int BookingId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            BookingController _bc = new BookingController();

            VM_BookingDetails bkDeials = _bc.GetBookingDetails(branchId, BookingId);
            Session["BranchId"] = branchId;
            return PartialView("_Invoice", bkDeials);

        }
        ///Currency
        ///
        public ActionResult SetCurrencyExchange()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            CurrencyController _curr = new CurrencyController();
            IEnumerable<AvailableCurrency> avlCurr = _curr.AvailableCurrency(branchId);
            var bCurr = from a in avlCurr
                        where a.isBusinessCurrency == true
                        select a;
            ViewBag.BusinessCurrencyName = bCurr.Select(c => c.CurrencyName).SingleOrDefault();
            ViewBag.BusinessCurrencyId = bCurr.Select(c => c.CurrencyId).SingleOrDefault();
            ViewBag.BusinessCurrencySymbol = bCurr.Select(c => c.CurrencySymbol).SingleOrDefault();
            ViewBag.BusinessCurrencyCode = bCurr.Select(c => c.CurrencyCode).SingleOrDefault();
            ViewBag.BranchId = branchId;
            var ExCurrData = from a in avlCurr
                             where a.isBusinessCurrency == false
                             select a;

            //List<SelectListItem> rlitems = new List<SelectListItem>();
            ////rlitems.Add(new SelectListItem { Text = "Select Business Currency", Value = "0" });
            //foreach (var item in bCurr)
            //{

            //    rlitems.Add(new SelectListItem { Text = item.CurrencyName, Value = item.CurrencyId.ToString()});
            //}
            //ViewBag.BusinessCurrency = rlitems;
            IEnumerable<CurrencyExchange> ExCurr = _curr.getExchnageRates(branchId);
            List<CurrResp> lstData = new List<CurrResp>();

            foreach (var x in ExCurrData)
            {
                var tData = (from a in ExCurr
                             where a.ExchangeCurrencyId == x.CurrencyId
                             select a
                                     ).SingleOrDefault();
                lstData.Add(new CurrResp
                {

                    CurrencyId = x.CurrencyId,
                    CurrencyName = x.CurrencyName,
                    CurrencySymbol = x.CurrencySymbol,
                    ExchangeValue = (tData == null) ? 0 : tData.ExchangeValue,
                    CurrExchangeId = (tData == null) ? 0 : tData.CurrExchangeId
                }); ;
            }


            ViewBag.ExchangeCurrency = lstData;
            ViewBag.CurrExchangeId = lstData.First().CurrExchangeId;


            return View();
        }
        public bool SaveCurrencyExchange(IEnumerable<CurrencyExchange> exCurrEntitis)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());


            CurrencyController _curr = new CurrencyController();
            bool rtnVal = _curr.SetExchnageRates(exCurrEntitis);
            //return RedirectToAction("SetCurrencyExchnage", "Home");
            return rtnVal;
        }

        public ActionResult MoneyExchange()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            CurrencyController _curr = new CurrencyController();
            IEnumerable<AvailableCurrency> avlCurr = _curr.AvailableCurrency(branchId);
            var bCurr = from a in avlCurr
                        where a.isBusinessCurrency == true
                        select a;
            ViewBag.BusinessCurrencyName = bCurr.Select(c => c.CurrencyName).SingleOrDefault();
            ViewBag.BusinessCurrencyId = bCurr.Select(c => c.CurrencyId).SingleOrDefault();
            ViewBag.BusinessCurrencySymbol = bCurr.Select(c => c.CurrencySymbol).SingleOrDefault();
            ViewBag.BusinessCurrencyCode = bCurr.Select(c => c.CurrencyCode).SingleOrDefault();
            ViewBag.BranchId = branchId;
            var ExCurrData = from a in avlCurr
                             where a.isBusinessCurrency == false
                             select a;


            IEnumerable<CurrencyExchange> ExCurr = _curr.getExchnageRates(branchId);
            List<CurrResp> lstData = new List<CurrResp>();

            foreach (var x in ExCurrData)
            {
                var tData = (from a in ExCurr
                             where a.ExchangeCurrencyId == x.CurrencyId
                             select a
                                     ).SingleOrDefault();
                lstData.Add(new CurrResp
                {

                    CurrencyId = x.CurrencyId,
                    CurrencyName = x.CurrencyName,
                    CurrencySymbol = x.CurrencySymbol,
                    ExchangeValue = (tData == null) ? 0 : tData.ExchangeValue,
                    CurrExchangeId = (tData == null) ? 0 : tData.CurrExchangeId
                }); ;
            }
            List<SelectListItem> rlitems = new List<SelectListItem>();
            rlitems.Add(new SelectListItem { Text = "Select Exchange Currency", Value = "0" });
            foreach (var item in lstData)
            {

                rlitems.Add(new SelectListItem { Text = item.CurrencyName, Value = item.CurrencyId.ToString() + "-" + item.ExchangeValue });
            }

            ViewBag.ExchangeCurrency = rlitems;

            return View();
        }

        public bool SaveExchangeTrans(ExchangeTransaction exCurrEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());


            CurrencyController _curr = new CurrencyController();
            bool rtnVal = _curr.SaveExchangeTrans(exCurrEntity);

            return rtnVal;
        }
        public ActionResult TransExchange(int? page, int? pSize)
        {
            if (Session.Keys.Count != 0)
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
                       new SelectListItem() { Value="2", Text= "2"},
                        new SelectListItem() { Value="5", Text= "5"},
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15"},
                        new SelectListItem() { Value="20", Text= "20" },
                     };

                int branchId = int.Parse(Session["BranchId"].ToString());
                CurrencyController _curr = new CurrencyController();
                IEnumerable<ExchangeTransaction> entrans = _curr.GetExchangeTrans(branchId);

                return View("TransExchange", entrans.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }


        ////Restaurant
       public ActionResult Restaurant(int? page, int? pSize)
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
                       new SelectListItem() { Value="2", Text= "2"},
                        new SelectListItem() { Value="5", Text= "5"},
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15"},
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            int branchId = int.Parse(Session["BranchId"].ToString());
            RestaurantController _rt = new RestaurantController();
            IEnumerable<RestaurantModel> entrans = _rt.GetRestaurants(branchId);

            return View("RestaurantList", entrans.ToPagedList(pageNumber, (int)DefaultPageSize));
        }
        public ActionResult AddRestaurant(int RestaurantId=0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            FloorController _fc = new FloorController();
            IEnumerable<Floor> fl=_fc.GetFloors(branchId);

           

            RestaurantModel rts = new RestaurantModel();
            RestaurantController _rt = new RestaurantController();
            if (RestaurantId > 0)
            {
                rts = _rt.GetRestaurant(branchId, RestaurantId);
                ViewBag.NoOfTables = rts.NoOfTable;
                ViewBag.Action = "EDIT";
            }
            else
            {
                ViewBag.Action = "ADD";
                ViewBag.NoOfTables = 0;
            }
          

            List<SelectListItem> rlitems = new List<SelectListItem>();
            rlitems.Add(new SelectListItem { Text = "Select a Floor", Value = "0" });
            foreach (var item in fl)
            {
                bool tmpSel=false;
                if (RestaurantId > 0) { 
                    if (item.FloorId == rts.FloorId)
                    {
                        tmpSel = true;
                    }
                }
                rlitems.Add(new SelectListItem { Text = item.FloorNumber, Value = item.FloorId.ToString(),Selected= tmpSel });
            }

            ViewBag.FLCombo = rlitems;

            ViewBag.BranchId = branchId;

            return View("Restaurant", rts);
        }

        public PartialViewResult _RestaurantTables(int nTables=0,int RestaurantId=0)    
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            int existingtables = 0;
            //IEnumerable<RestaurantTables> rts;
            List<RestaurantTables> rts1= new List<RestaurantTables>();
            RestaurantController _rt = new RestaurantController();

            if (RestaurantId > 0)
            {
                rts1 = _rt.GetRestaurantTables(RestaurantId).ToList();
                existingtables = rts1.Count();
            }
            
            if (existingtables > nTables)
            {
                int tobeRemovefrom = existingtables - nTables;
                rts1.RemoveRange(nTables, tobeRemovefrom);
                
            }
            if (existingtables < nTables)
            {
                int tobeAdd = nTables - existingtables;
                for (int i = 0; i < tobeAdd; i++)
                {
                    RestaurantTables restaurantTables = new RestaurantTables();
                    restaurantTables.RestaurantId = RestaurantId;
                    restaurantTables.NoOfSeat = 0;
                    rts1.Add(restaurantTables);
                }
            }

            ViewBag.nTables = nTables;

            return PartialView("_RestaurantTables", rts1);

        }
        public bool SaveRestaurant12(RestaurantModel restroEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
            }
                RestaurantController _rt = new RestaurantController();
            bool rtnVal = _rt.SaveRestaurant(restroEntity);

            return rtnVal;
        }
        public bool SaveRestaurant(HttpPostedFileBase[] httpPostedFileBase, string restroEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            RestaurantModel rstModel=JsonConvert.DeserializeObject<RestaurantModel>(restroEntity);
            List<ImageMaster> lstImages = new List<ImageMaster>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                   
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(Request.Files[i].InputStream))
                    {
                        bytes = br.ReadBytes(Request.Files[i].ContentLength);
                    }
                    if (bytes.Length > 0)
                    {
                        ImageMaster img = new ImageMaster();
                        img.BranchId=branchId;
                        img.ImageContentType = Request.Files[i].ContentType;
                        img.ImageName = Request.Files[i].FileName;
                        img.ImageTypeId = 2;
                        img.isActive = true;
                        img.ImageData=bytes;
                        lstImages.Add(img);

                    }
                }
                rstModel.ImageData = lstImages;
            }
            
            
            RestaurantController _rt = new RestaurantController();
            bool rtnVal= _rt.SaveRestaurant(rstModel);

            return rtnVal;
        }

        public ActionResult ViewRestaurant(int RestaurantId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
           
            RestaurantModel rts = new RestaurantModel();
            RestaurantController _rt = new RestaurantController();
           
                rts = _rt.GetRestaurant(branchId, RestaurantId);
                ViewBag.NoOfTables = rts.NoOfTable;
               
           
            ViewBag.BranchId = branchId;

            return View("ViewRestaurant", rts);
        }

        public PartialViewResult _RestaurantImages(int RestaurantId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            List<ImageMaster> imgList = new List<ImageMaster>();
            RestaurantController _rt = new RestaurantController();
            imgList = _rt.GetRestaurantImagess(RestaurantId).ToList();
            return PartialView("_RestaurantImages", imgList);
           

        }
        public ActionResult RestaurantMenu(int RestaurantId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            ViewBag.RestaurantId = RestaurantId;

            RestaurantController _rt = new RestaurantController();
            IEnumerable<RestaurantMenu> rstMenus = _rt.GetRestaurantMenus(RestaurantId);
            


            ViewBag.BranchId = branchId;

            return View("RestaurantMenuList", rstMenus);
        }
        public ActionResult AddRestaurantMenu(int RestaurantId,int RestaurantMenuId=0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            if (RestaurantId > 0)
            {
                ViewBag.RestaurantId = RestaurantId;
            }
            RestaurantMenu rsm = new RestaurantMenu();
            RestaurantController _rt = new RestaurantController();
            if (RestaurantMenuId > 0)
            {

                rsm = _rt.GetRestaurantMenu(RestaurantMenuId);
            }
            //RestaurantModel rts = ne w RestaurantModel();
            //

            //rts = _rt.GetRestaurant(branchId, RestaurantId);
            //ViewBag.NoOfTables = rts.NoOfTable;


            ViewBag.BranchId = branchId;

            return View("RestaurantMenu", rsm);
        }
        public ActionResult EditRestaurantMenu(int RestaurantId,int RestaurantMenuId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
           
            if (RestaurantId >0)
            {
                ViewBag.RestaurantId= RestaurantId;
            }
            RestaurantMenu rsm = new RestaurantMenu();
            RestaurantController _rt = new RestaurantController();
            if (RestaurantMenuId > 0)
            {

                rsm = _rt.GetRestaurantMenu(RestaurantMenuId);
            }
            //RestaurantModel rts = ne w RestaurantModel();
            //

            //rts = _rt.GetRestaurant(branchId, RestaurantId);
            //ViewBag.NoOfTables = rts.NoOfTable;


            ViewBag.BranchId = branchId;

            return View("EditRestaurantMenu", rsm);
        }

        public bool SaveRestaurantMenu(RestaurantMenu restroMenuEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

           
            RestaurantController _rt = new RestaurantController();
            bool rtnVal = _rt.SaveRestaurantMenu(restroMenuEntity);

            return rtnVal;
        }

        public ActionResult FoodCart()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            ViewBag.BranchId = branchId; 
            int RestaurantId = 1;
            int existingtables = 0;
            //IEnumerable<RestaurantTables> rts;
            List<RestaurantTables> rts1 = new List<RestaurantTables>();
            RestaurantController _rt = new RestaurantController();
            IEnumerable<RestaurantModel> lstModel = _rt.GetRestaurants(branchId);

            List<SelectListItem> rlitems = new List<SelectListItem>();
            rlitems.Add(new SelectListItem { Text = "Select a Restaurant", Value = "0" });
            foreach (var item in lstModel)
            {
                rlitems.Add(new SelectListItem { Text = item.Name, Value = item.RestaurantId.ToString() });
            }
            ViewBag.RestroCmoboModel = rlitems;
            
            
            return View("FoodCart");
        }

        public PartialViewResult _RestaurantMenuItems(int RestaurantId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RestaurantController _rt = new RestaurantController();
            IEnumerable<RestaurantMenu> rsm = _rt.GetRestaurantMenus(RestaurantId);

            return PartialView("_RestaurantMenuItems", rsm);


        }
        public PartialViewResult _FoodCartTables( int RestaurantId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
           
            //IEnumerable<RestaurantTables> rts;
            List<RestaurantTables> rts1 = new List<RestaurantTables>();
            RestaurantController _rt = new RestaurantController();
             rts1 = _rt.GetRestaurantTables(RestaurantId).ToList();
             //ViewBag.nTables = nTables;

            return PartialView("_FoodCartTables", rts1);

        }

        public PartialViewResult _FoodCartRoomServices(int RestaurantId = 0)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            //IEnumerable<RestaurantTables> rts;
            List<RestaurantRoomService> rts1 = new List<RestaurantRoomService>();
            RestaurantController _rt = new RestaurantController();
            rts1 = _rt.GetRestaurantRoomServices(RestaurantId, branchId).ToList();
            //ViewBag.nTables = nTables;

            return PartialView("_FoodCartRoomServices", rts1);

        }

        public PartialViewResult _FoodCartItems(int RestaurantId = 0,int tableId=0,bool isRMS=false)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());

            //IEnumerable<RestaurantTables> rts;
          
            RestaurantController _rt = new RestaurantController();
            
            IEnumerable<BillingDetails> rts1 = _rt.GetParkItems(RestaurantId,tableId, isRMS);
           

            return PartialView("_FoodCartItems", rts1);

        }

        public bool SaveFoodCart(BillingMaster billingmasterEntity)
        {
            bool rtnVal;
            int branchId = int.Parse(Session["BranchId"].ToString());

            RestaurantController _rt = new RestaurantController();

            rtnVal = _rt.SaveFoodCart(billingmasterEntity);

            return rtnVal;
        }
        public bool ReleaseTable(int RestaurantId,int tableId)
        {
            bool rtnVal;
            int branchId = int.Parse(Session["BranchId"].ToString());

            RestaurantController _rt = new RestaurantController();

            rtnVal = _rt.releaseTable(RestaurantId, tableId);

            return rtnVal;
        }

        [HttpGet]
        public JsonResult AutoCompleteServiceProviders(int RestaurantId)
        {
           
           

            List<string> servicesList = new List<string>();
            RestaurantController _rt = new RestaurantController();
            IEnumerable<RestaurantMenu> rsm = _rt.GetRestaurantMenus(RestaurantId);
            foreach (var item in rsm)
            {
                foreach(var menuHeading in item.MenuHeading) {

                    foreach (var menuItem in menuHeading.MenuItems)
                    {
                        servicesList.Add((menuItem.MenuItemName + "-" + menuItem.ItemPrice+"~"+ menuItem.MenuItemId ));

                    }
                }
               
            }

            
            return Json(servicesList.ToArray(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCalendarData(int selectedMonth,int selectedYear)
        {
            // Initialization.  
            JsonResult result = new JsonResult();

            try
            {
                // Loading.  

                List<AvailabilityCalendar> data = GetCalData(selectedYear, selectedMonth);
                List<AvailabilityCalendar> data1 = new List<AvailabilityCalendar>()
                {
                    new AvailabilityCalendar()
                    {
                     Sr=1,
                     Title="Room Status",
                     Start_Date="2024/04/06",
                     End_Date="2024/04/06",
                     Desc="Available",
                     backgroundColor="#065a18eb !important",
                     textColor="#000000 !important",
                     AvalDate= new DateTime(2024,04,06),
                     AvailableRooms=20,
                     BookedRooms=10
                    },
                     new AvailabilityCalendar()
                    {
                      Sr=1,
                     Title="Room Status",
                     Start_Date="2024/04/07",
                     End_Date="2024/04/07",
                     Desc="Available",
                     backgroundColor="#065a18eb !important",
                     textColor="#000000 !important",
                     AvalDate= new DateTime(2024,04,06),
                     AvailableRooms=20,
                     BookedRooms=10
                    },
                      new AvailabilityCalendar()
                    {
                     Sr=3,
                     Title="First",
                     Start_Date="2004/04/08",
                     End_Date="2004/04/08",
                     Desc="Available",
                     AvalDate= new DateTime(2024,04,08),
                     AvailableRooms=20,
                     BookedRooms=10
                    },
                       new AvailabilityCalendar()
                    {
                      Sr=4,
                     Title="First",
                     Start_Date="2004/04/09",
                     End_Date="2004/04/09",
                     Desc="Available",
                     AvalDate= new DateTime(2024,04,09),
                     AvailableRooms=20,
                     BookedRooms=10
                    },
                        new AvailabilityCalendar()
                    {
                      Sr=5,
                     Title="First",
                     Start_Date="2004/04/10",
                     End_Date="2004/04/10",
                     Desc="Available",
                     AvalDate= new DateTime(2024,04,06),
                     AvailableRooms=20,
                     BookedRooms=10
                    },
                };
                

                // Processing.  
                result = this.Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Return info.  
            return result;
        }
        public List<AvailabilityCalendar> GetCalData(int year, int month)
        {
            var dates = new List<DateTime>();

            IEnumerable<DashBoardData> ds = new List<DashBoardData>();
            BookingController _bk = new BookingController();
            int branchId = int.Parse(Session["BranchId"].ToString());
            ds = _bk.CalendarData(branchId);

            List<AvailabilityCalendar> data = new List<AvailabilityCalendar>();
            int i = 1;
            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
            {
                foreach(var item in ds)
                {
                    if (item.AvailabilityDate == date.ToString("MM-dd-yyyy"))
                    {
                        AvailabilityCalendar kk = new AvailabilityCalendar()
                        {
                            Sr = 1,
                            Title = "Room Status",
                            Start_Date = item.AvailabilityDate,
                            End_Date = item.AvailabilityDate,
                            Desc = "Available",
                            backgroundColor = "#065a18eb !important",
                            textColor = "#000000 !important",
                            AvalDate = new DateTime(2024, 04, 06),
                            AvailableRooms = item.AvailableRooms,
                            BookedRooms = item.BookedRooms
                        };
                        data.Add(kk);
                    }
                    //else
                    //{
                    //    AvailabilityCalendar kk = new AvailabilityCalendar()
                    //    {
                    //        Sr = 1,
                    //        Title = "Room Status",
                    //        Start_Date = date.ToString("yyyy-MM-dd"),
                    //        End_Date = date.ToString("yyyy-MM-dd"),
                    //        Desc = "Available",
                    //        backgroundColor = "#065a18eb !important",
                    //        textColor = "#000000 !important",
                    //        AvalDate = new DateTime(2024, 04, 06),
                    //        AvailableRooms =item.NoOfRooms,
                    //        BookedRooms = 0
                    //    };
                    //    data.Add(kk);
                    //}
                }
                
               
                i++;
            }

            return data;
        }
    }
}

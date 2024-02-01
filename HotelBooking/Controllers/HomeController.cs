using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HotelBooking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            LoginController loginController = new LoginController();

            if (Session.Keys.Count != 0) {
                int userid = (int)Session["UserId"];
                UserLoginResponse model = loginController.UserLogin(userid);

                Session["Logged_In"] = "LoggedIn";
                Session["UserLoginResponse"] = model;
                Session["CompanyId"] = model.CompanyId;
                Session["BranchId"] = model.BranchId;

                return View();
            }
            else
            {
                Session["ErrorMessage"] = "Please check your credential";
                return RedirectToAction("Index", "unProHome");
            }



        }
        public ActionResult Logout()
        {
            Session["Logged_In"] = "";
            Session["UserId"] = "";
            Session["ErrorMessage"] = "";
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


        public ActionResult RoomType(int? page,int? pSize=2)
        {
            if (Session.Keys.Count != 0)
            {
                int? DefaultPageSize = 10;
                IEnumerable<RoomType> RTListModel = new List<RoomType>();
                RoomTypeController RTC = new RoomTypeController();
                int branchId = int.Parse(Session["BranchId"].ToString());
                if (pSize!=null){
                    DefaultPageSize = pSize;
                }
               
                int pageNumber = page ?? 1;

                RTListModel = RTC.GetRoomTypes(branchId);
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
                List <SelectListItem> kk = new List<SelectListItem>();
                int pc=rmlist.PageCount;
                for (int i = 1;i<=pc;i++)
                {
                    kk.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
                ViewBag.pc = kk;

                return View(rmlist);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult AddRoomType()
        {
            if (Session.Keys.Count != 0)
            {
                List<Amenities> amListModel = new List<Amenities>();
                AmenitiesController _am = new AmenitiesController();
                int branchId = int.Parse(Session["BranchId"].ToString());
                amListModel = _am.GetAmenities(branchId).Where(i => i.IsActive == true).ToList();

                RoomType mrtModel = new RoomType();
                mrtModel.AmenitiesData = amListModel;
                Session["BranchId"] = branchId;
                return View(mrtModel);
            }
            else {
                return RedirectToAction("index", "unProHome");
            }

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveRoomType(RoomType roomTypeEntiry)
        {
            if (Session.Keys.Count != 0)
            {
                RoomTypeController RTC = new RoomTypeController();
                int branchId = int.Parse(Session["BranchId"].ToString());
                roomTypeEntiry.BranchId = branchId;
                bool trn = RTC.AddRoomType(roomTypeEntiry);
                Session["BranchId"] = branchId;
                return RedirectToAction("RoomType");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }

        public ActionResult GetAmenities(int? page)
        {
            if (Session.Keys.Count != 0)
            {
                IEnumerable<Amenities> amListModel = new List<Amenities>();
                AmenitiesController _am = new AmenitiesController();
                int branchId = int.Parse(Session["BranchId"].ToString());

                amListModel = _am.GetAmenities(branchId);
                int pageSize = 5;
                int pageNumber = page ?? 1;

                Session["BranchId"] = branchId;
                return View(amListModel.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }

        public ActionResult AddAmenities()
        {
            if (Session.Keys.Count != 0)
            {
                
                Amenities am = new Amenities();
                int branchId = int.Parse(Session["BranchId"].ToString());

                am.BranchId = branchId;
                am.DateCreated = DateTime.Now;
               
                Session["BranchId"] = branchId;
                return View(am);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
           
            
        }

        [HttpPost]
        public ActionResult SaveAmenities(Amenities amenitiesEntity)
        {
            if (Session.Keys.Count != 0)
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
            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }

        public ActionResult RoomTypeImage(int RoomTypeId, int? page)
        {
            if (Session.Keys.Count > 0)
            {
                RoomTypeImageController _RTI = new RoomTypeImageController();
                ViewBag.RoomTypeId = RoomTypeId;
                IEnumerable<RoomeTypeImages> RTIS = _RTI.GetRoomTypeImages(RoomTypeId);

                int pageSize = 5;
                int pageNumber = page ?? 1;

               
                return View(RTIS.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        [HttpPost]
        public ActionResult AddRoomTypeImage(FormCollection form)
        {

            if (Session.Keys.Count > 0)
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


                RD = _RTI.SaveRoomTypeImage(FVM);

                return RedirectToAction("RoomTypeImage", "Home", new { RoomTypeId = RoomTyepId });
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
            
        }

        public ActionResult Floor()
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                ViewBag.branchId = branchId;
                Floor fl = new Floor();
                return View("MangeFloor", fl);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        [HttpPost]
        public ActionResult SaveFloor(Floor floorEntity)
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                FloorController _fl = new FloorController();
                bool rtn = _fl.AddFloor(floorEntity);
                return RedirectToAction("FloorDetails");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult FloorDetails()
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                FloorController _fl = new FloorController();
                IEnumerable<Floor> flrs = _fl.GetFloors(branchId);
                return View("FloorDetails", flrs);
            }
            
                else
                {
                    return RedirectToAction("index", "unProHome");
                }
            
        }

        public ActionResult Room(int? page, int? pSize = 2)
        {
            if (Session.Keys.Count != 0)
            {
                int? DefaultPageSize = 10;
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomController _rm = new RoomController();
                IEnumerable<Room> rmModel = _rm.GetRooms(branchId);
                FloorController _fl = new FloorController();
                IEnumerable<Floor> flrs = _fl.GetFloors(branchId);


                List<SelectListItem> floortems = new List<SelectListItem>();
                floortems.Add(new SelectListItem { Text = "Select a Floor", Value = "0" });
                foreach (Floor item in flrs)
                {
                    floortems.Add(new SelectListItem { Text = item.FloorNumber + "-" + item.Description, Value = item.FloorId.ToString() + "~" + item.FloorNumber + "-" + item.Description });
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

                rtm = RTC.GetRoomTypes(branchId);

                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;

                List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
                RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
                foreach (RoomType item in rtm)
                {
                    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title });
                }
                ViewBag.RTComboModel = RoomTypeitems;
                ViewBag.RoomNumber = "";

                return View(rmModel.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult SaveRoom()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            string[] tempArr = Request.Form["FloorComboModel"].Split('~');
            string[] tempRTArr = Request.Form["RTComboModel"].Split('~');
            int rtmid = int.Parse(tempRTArr[0]);
            string rtmName = tempRTArr[1].ToString();
            int flId = int.Parse(tempArr[0]);
            string flName = tempArr[1].ToString();
            Room rm = new Room();
            rm.BranchId = branchId;
            rm.RoomNumber = int.Parse(Request.Form["RoomNumber"].ToString());
            rm.floor = flId;
            rm.FloorName = flName;
            rm.RoomTypeId = rtmid;
            rm.RoomTypeName = rtmName;

            RoomController _rm = new RoomController();

            bool rtnVal = _rm.AddRoom(rm);
            return RedirectToAction("Room");
        }
        public ActionResult PaidServices(int? page, int? pSize = 2)
        {
            if (Session.Keys.Count != 0)
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
                PaidServicesController _psc = new PaidServicesController();
                IEnumerable<PaidServices> rtm = _psc.GetpadServices(branchId);
                return View(rtm.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }


        public ActionResult AddPaidServices()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId);
            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() });
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
                PriceTypeitems.Add(new SelectListItem { Text = item.PriceTypeTitle, Value = item.PriceTypeId.ToString() });
            }
            ViewBag.PriceTypeModel = PriceTypeitems;
            PaidServices ps = new PaidServices();
            ps.BranchId = branchId;
            return View(ps);
        }
        [HttpPost]
        public ActionResult SavePaidServices(PaidServices paidServiceEntity)
        {

            PaidServicesController _psc = new PaidServicesController();
            bool rtnVal = _psc.AddPaidServices(paidServiceEntity);
            return RedirectToAction("PaidServices");
        }

        public ActionResult PriceManager(int? page, int? pSize = 2)
        {
            if (Session.Keys.Count != 0)
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
                pmController _psc = new pmController();
                IEnumerable<PriceManager> pcm = _psc.GetAllPrice(branchId);
                return View(pcm.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult AddPrice()
        {
            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomTypeController RTC = new RoomTypeController();

                IEnumerable<RoomType> rtm = new List<RoomType>();

                rtm = RTC.GetRoomTypes(branchId);
                List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
                RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
                foreach (RoomType item in rtm)
                {
                    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() });
                }
                ViewBag.RTComboModel = RoomTypeitems;
                return View();
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        
        public PartialViewResult _RegularPrice()
        {

           
            int branchId = int.Parse(Session["BranchId"].ToString());
            
            PriceManager ps = new PriceManager();
            ps.BranchId = branchId;
            return PartialView("_RegularPrice", ps);
            
        }
       
        public PartialViewResult _SpecialPrice()
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomTypeController RTC = new RoomTypeController();

            IEnumerable<RoomType> rtm = new List<RoomType>();

            rtm = RTC.GetRoomTypes(branchId);
            List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
            RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
            foreach (RoomType item in rtm)
            {
                RoomTypeitems.Add(new SelectListItem { Text = item.Title.Trim(), Value = item.RoomTypeId.ToString() });
            }
            ViewBag.RTComboModel = RoomTypeitems;


            PriceManager ps = new PriceManager();
            ps.BranchId = branchId;
            return PartialView("_SpecialPrice", ps);
        }

        [HttpPost]
        public ActionResult SaveRegularPrice(PriceManager pmEntity)
        {

            pmController pmc = new pmController();
            bool rtnVal = pmc.AddPrice(pmEntity);
            return RedirectToAction("PriceManager");
        }

    }
}

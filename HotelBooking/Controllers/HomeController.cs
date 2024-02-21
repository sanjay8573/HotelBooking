using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using Microsoft.Ajax.Utilities;
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


        public ActionResult RoomType(int? page,int? pSize)
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
        public ActionResult AddRoomType(int RoomTypeId = 0)
        {
            if (Session.Keys.Count != 0)
            {
                List<Amenities> amListModel = new List<Amenities>();
                AmenitiesController _am = new AmenitiesController();
                RoomTypeController _rt = new RoomTypeController();
                int branchId = int.Parse(Session["BranchId"].ToString());
                amListModel = _am.GetAmenities(branchId).Where(i => i.isDeleted == false && i.IsActive==true).ToList();

                RoomType mrtModel = new RoomType();
               
                Session["BranchId"] = branchId;

                if (RoomTypeId > 0)
                {
                    mrtModel= _rt.GetRoomTypes(branchId).Where(r=>r.RoomTypeId== RoomTypeId).SingleOrDefault();
                }
                mrtModel.AmenitiesData = amListModel;
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

        public ActionResult DelRoomType(int RoomTypeId = 0)
        {
            if (RoomTypeId > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomTypeController   _rt = new RoomTypeController();
                _rt.DeleteRoomtype(branchId, RoomTypeId);
                return RedirectToAction("RoomType");
            }

            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }

        public ActionResult GetAmenities(int? page, int? pSize )
        {
            if (Session.Keys.Count != 0)
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

                amListModel = _am.GetAmenities(branchId).Where(d => d.isDeleted == false ); ;
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }
                
                int pageNumber = page ?? 1;

                Session["BranchId"] = branchId;
                return View(amListModel.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }

        public ActionResult AddAmenities(int AmenitisId=0)
        {
            if (Session.Keys.Count != 0)
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

        public ActionResult DelAmenity(int AmenitisId = 0)
        {
            if (AmenitisId > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                AmenitiesController _am = new AmenitiesController();
                _am.DeleteAmenities(branchId, AmenitisId);
                return RedirectToAction("GetAmenities");
            }

            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }
        public ActionResult RoomTypeImage(int RoomTypeId, int? page, int? pSize )
        {
            int? DefaultPageSize = 10;
            if (Session.Keys.Count > 0)
            {
                RoomTypeImageController _RTI = new RoomTypeImageController();
                ViewBag.RoomTypeId = RoomTypeId;
                IEnumerable<RoomeTypeImages> RTIS = _RTI.GetRoomTypeImages(RoomTypeId).Where(d=>d.isActive==true && d.isDeleted==false);
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
                return View(RTIS.ToPagedList(pageNumber, (int)DefaultPageSize));
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

        public ActionResult Floor(int FloorId=0)
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                
                Floor fl = new Floor();
                fl.BranchId = branchId;
                FloorController fc = new FloorController();
                if (FloorId > 0)
                {
                    fl = fc.GetFloors(branchId).Where(f => f.FloorId == FloorId).SingleOrDefault();
                }
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

       
        public ActionResult DelFloor(int FloorId)
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                FloorController _fl = new FloorController();
                _fl.DelFloor(FloorId);
                return RedirectToAction("FloorDetails");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult FloorDetails(int? page, int? pSize )
        {
            int? DefaultPageSize = 10;
            if (Session.Keys.Count > 0)
            {
                
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
                FloorController _fl = new FloorController();
                IEnumerable<Floor> flrs = _fl.GetFloors(branchId).Where(d => d.isDeleted == false );
                return View("FloorDetails", flrs.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            
                else
                {
                    return RedirectToAction("index", "unProHome");
                }
            
        }

        public ActionResult EditFloor(int FloorId)
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                ViewBag.branchId = branchId;
                FloorController fc = new FloorController();

                Floor fl = fc.GetFloors(branchId).Where(f => f.FloorId == FloorId).SingleOrDefault();
                return View("MangeFloor", fl);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult Room(int? page, int? pSize )
        {
            if (Session.Keys.Count != 0)
            {
                int? DefaultPageSize = 10;
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomController _rm = new RoomController();
                IEnumerable<Room> rmModel = _rm.GetRooms(branchId).Where(d=>d.isDeleted==false);
                
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

                //List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
                //RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
                //foreach (RoomType item in rtm)
                //{
                //    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title });
                //}
                //ViewBag.RTComboModel = RoomTypeitems;
                ViewBag.RoomNumber = "";

                return View(rmModel.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult AddRoom()
        {
            if (Session.Keys.Count != 0)
            {
               
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomController _rm = new RoomController();
                    Room rmModel =  new Room();
                rmModel.BranchId = branchId;
                FloorController _fl = new FloorController();
                IEnumerable<Floor> flrs = _fl.GetFloors(branchId).Where(d=>d.isDeleted==false && d.isActive==true);


                List<SelectListItem> floortems = new List<SelectListItem>();
                floortems.Add(new SelectListItem { Text = "Select a Floor", Value = "0" });
                foreach (Floor item in flrs)
                {
                    floortems.Add(new SelectListItem { Text = item.FloorNumber + "-" + item.Description, Value = item.FloorId.ToString()  });
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

                rtm = RTC.GetRoomTypes(branchId).Where(d=>d.isDeleted==false && d.isActive==true);


               

                List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
                RoomTypeitems.Add(new SelectListItem { Text = "Select a Room Type", Value = "0" });
                foreach (RoomType item in rtm)
                {
                    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title });
                }
                ViewBag.RTComboModel = RoomTypeitems;
                ViewBag.RoomNumber = "";

                return View(rmModel);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult EditRoom(int Roomid)
        {
            if (Session.Keys.Count != 0)
            {

                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomController _rm = new RoomController();
                Room rmModel = _rm.GetRooms(branchId).Where(r => r.RoomId == Roomid).SingleOrDefault();

                rmModel.BranchId = branchId;
                FloorController _fl = new FloorController();
                IEnumerable<Floor> flrs = _fl.GetFloors(branchId).Where(d=>d.isDeleted==false && d.isActive==true);


                List<SelectListItem> floortems = new List<SelectListItem>();
                floortems.Add(new SelectListItem { Text = "Select a Floor", Value = "0" });
                foreach (Floor item in flrs)
                {
                    bool selected = item.FloorId == rmModel.floor;
                    floortems.Add(new SelectListItem { Text = item.FloorNumber + "-" + item.Description, Value = item.FloorId.ToString(),Selected=selected });
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
                    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString() + "~" + item.Title,Selected= selected });
                }
                ViewBag.RTComboModel = RoomTypeitems;
                ViewBag.RoomNumber = "";

                return View(rmModel);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
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

            return Json("Success", JsonRequestBehavior.AllowGet);
           // return RedirectToAction("Room");
        }
        public ActionResult SaveEditedRoom(Room roomEntity)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();
            
                bool rtnVal = _rm.AddRoom(roomEntity);
           

            
            return RedirectToAction("Room");
        }
        public ActionResult DelRoom(int RoomId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rm = new RoomController();

            _rm.DeleteRoom(RoomId);

            return RedirectToAction("Room");
        }

        public ActionResult PaidServices(int? page, int? pSize )
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
                IEnumerable<PaidServices> rtm = _psc.GetpadServices(branchId).Where(d=>d.isDeleted==false);
                return View(rtm.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }


        public ActionResult AddPaidServices(int PaidServiceId=0)
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
                if(PaidServiceId>0)
                {
                    if (ps.RoomTypeId.Contains(item.RoomTypeId.ToString()))
                    {
                        slt = true;
                    }
                }
                
                RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString(),Selected=slt });
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
                PriceTypeitems.Add(new SelectListItem { Text = item.PriceTypeTitle, Value = item.PriceTypeId.ToString(),Selected=slt });
            }
            ViewBag.PriceTypeModel = PriceTypeitems;
            
            return View(ps);
        }
        public ActionResult DelPaidServices(int PaidServiceId = 0)
        {
            if (Session.Keys.Count != 0)
            {
                PaidServicesController _ps = new PaidServicesController();
                _ps.DeletePaidServices(PaidServiceId);
                return RedirectToAction("PaidServices");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        

        [HttpPost]
        public ActionResult SavePaidServices(PaidServices paidServiceEntity)
        {

            PaidServicesController _psc = new PaidServicesController();
            bool rtnVal = _psc.AddPaidServices(paidServiceEntity);
            return RedirectToAction("PaidServices");
        }

        
        
        public ActionResult PriceManager(int? page, int? pSize )
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
                IEnumerable<PriceManager> pcm = _psc.GetAllPrice(branchId).Where(d => d.isDeleted == false);
                return View(pcm.ToPagedList(pageNumber, (int)DefaultPageSize));
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult AddPrice(int PriceManagerId = 0,int tab=1)
        {
            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomTypeController RTC = new RoomTypeController();
                PriceManager PM = new PriceManager();
                PM.BranchId = branchId;
                if (PriceManagerId > 0)
                {
                    pmController _pm = new pmController();
                    PM = _pm.GetAllPrice(branchId).Where(p=>p.PriceManageId== PriceManagerId).SingleOrDefault();
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
                       if(PM.RoomTypeId==item.RoomTypeId)
                        {
                            slt = true;
                        }
                    }
                    RoomTypeitems.Add(new SelectListItem { Text = item.Title, Value = item.RoomTypeId.ToString(),Selected=slt });
                }
                ViewBag.RTComboModel = RoomTypeitems;
                ViewBag.SelectedTab = tab;
                ViewBag.PriceManageId = PM.PriceManageId;
                return View();
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
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
            return PartialView("_RegularPrice", ps);
            
        }
        public ActionResult DelPrice(int PriceManageId )
        {
            if (Session.Keys.Count != 0)
            {
                pmController _ps = new pmController();
                _ps.DeletePrice(PriceManageId);
                return RedirectToAction("PriceManager");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
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

            IEnumerable<SPM>  ps = _pm.GetAllSpecialPrice(branchId).Where(d=>d.isDeleted==false);

            int pageNumber = page ?? 1;
            ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

            return PartialView("_SpecialPriceGrid", ps.ToPagedList(pageNumber, (int)DefaultPageSize));
        }

        public ActionResult Coupon(int? page, int? pSize)
        {
            int? DefaultPageSize = 10;
            if (Session.Keys.Count > 0)
            {

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
                CouponController _cp = new CouponController();
                IEnumerable<Coupon> cpModel = _cp.GetCoupons(branchId);
                return View("Coupons", cpModel.ToPagedList(pageNumber, (int)DefaultPageSize));
            }

            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }
        public ActionResult AddCoupon(int couponId=0)
        {
            if (Session.Keys.Count != 0)
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
                if(couponId>0)
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
            else
            {
                return RedirectToAction("index", "unProHome");
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
            return RedirectToAction("Coupon");
        }

        public ActionResult DelCoupon(int couponId = 0)
        {
            if(couponId>0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                CouponController _cp = new CouponController();
                _cp.DeleteCoupon(branchId, couponId);
                return RedirectToAction("Coupon");
            }

            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }
    }
}

using HotelBooking.Model;
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
    public class BookingInterfaceController : Controller
    {
        // GET: BookingInterface
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Bookings(int? page, int? pSize )
        {
            if (Session.Keys.Count != 0)
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

                bksListModel = _bks.GetBookings(branchId);//.Where(d => d.isDeleted == false);
                Session["BranchId"] = branchId;

                ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };
                IPagedList<Booking> bkslist = bksListModel.ToPagedList(pageNumber, (int)DefaultPageSize);
               
                

                return View(bkslist);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult AddBooking(int bookingId=0)
        {
            if (Session.Keys.Count != 0)
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
                            if (item.RoomTypeId == bk.RoomTypeId)
                                slt = true;
                        }
                        RoomTypeitems.Add(new SelectListItem { Text = item.Title.Trim(), Value = item.RoomTypeId.ToString(), Selected = slt });
                    }
                    ViewBag.RTComboModel = RoomTypeitems;
                if (bookingId > 0)
                {
                    PaidServicesController _ps = new PaidServicesController();
                    IEnumerable<PaidServices> ps = _ps.GetPaidServicesByRoomType(bk.RoomTypeId).Where(i => i.isActive == true && i.BranchId == branchId && i.isDeleted == false);

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


                return View(bk);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public bool SaveBooking(BookingRequest bookingEntity)
        {
            BookingController _bk = new BookingController();
            return _bk.AddBooking(bookingEntity);
        }
        public enum BookingStatus
        {
            Select =0,
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
                                         Selected=(bk.ToString()==VMB.BookingStatus)
                                     };

            ViewBag.pmtStatus = from bk in pmtStatus
                                select new SelectListItem
                               {
                                   Text = bk.ToString(),
                                   Value = bk.ToString(),
                                    Selected = (bk.ToString() == VMB.PaymnentStatus)
                                };

            



            return  View(VMB);
        }


        public PartialViewResult _BookingDetails(string RoomTypeId, string cinDate, string coutDate)
        {

            PriceRequest pr = new PriceRequest();
            pr.CheckInDate = cinDate;
            pr.CheckOutDate = coutDate;
            pr.roomTypeId = int.Parse(RoomTypeId);
            int branchId = int.Parse(Session["BranchId"].ToString());

            BookingController _bc = new BookingController();
            IEnumerable<PriceResponse> _pr = _bc.GetPricesForNight(pr);


            return PartialView("_BookingDetails", _pr);

        }
        public PartialViewResult _Payments(int BranchId, int BookingId, int? page, int? pSize)
        {
            IPagedList<BookingPayments> bkplist=null;
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
        public PartialViewResult _AllocateRooms(int BookingId, int RoomTypeId)
        {
            int branchId = int.Parse(Session["BranchId"].ToString());
            RoomController _rc = new RoomController();

            IEnumerable<Room> rm = _rc.GetRoomsByRoomTypeId(branchId, RoomTypeId);
            List<SelectListItem> Roomitems = new List<SelectListItem>();
            Roomitems.Add(new SelectListItem { Text = "Select a Room", Value = "0" });
            foreach (Room item in rm)
            {
                Roomitems.Add(new SelectListItem { Text = item.RoomTypeName+"~"+ item.RoomNumber + "-"+ item.FloorName, Value = item.RoomId.ToString()});
            }
            ViewBag.RMComboModel = Roomitems;

            return PartialView("_AllocateRooms");

        }

        public PartialViewResult _Documents(int BookingId, int noPax,int noRooms, int? page, int? pSize)
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
            avlDocs.Add(new SelectListItem { Text = "Select Document Type", Value = "0",Selected=true });
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

        public PartialViewResult _PricePerNight(string RoomTypeId , string cinDate,string coutDate)
        {
            
            PriceRequest pr = new PriceRequest();
            pr.CheckInDate = cinDate;
            pr.CheckOutDate= coutDate;
            pr.roomTypeId= int.Parse(RoomTypeId);
            int branchId = int.Parse(Session["BranchId"].ToString());

            BookingController _bc = new BookingController();
            IEnumerable<PriceResponse> _pr = _bc.GetPricesForNight(pr);


            return PartialView("_PricePerNights", _pr);

        }
        public PartialViewResult _PaidServices(string RoomTypeId)
        {
           
            int roomTypeId = int.Parse(RoomTypeId);
            int branchId = int.Parse(Session["BranchId"].ToString());

            PaidServicesController _ps = new PaidServicesController();
            IEnumerable<PaidServices> ps = _ps.GetPaidServicesByRoomType(roomTypeId).Where(i=>i.isActive==true && i.BranchId== branchId && i.isDeleted==false);


            return PartialView("_PaidServices", ps);

        }


        [HttpPost]
        public decimal ApplyCoupon(int BranchId, string couponCode)
        {
            CouponController CC = new CouponController();
            CouponResponse cr= CC.ApplyCoupon(couponCode);

            return cr.CouponValue;
        }


        public ActionResult BookedRoom(int? page, int? pSize = 2)
        {
            if (Session.Keys.Count > 0)
            {
                int? DefaultPageSize = 10;
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;

                int branchId = int.Parse(Session["BranchId"].ToString());

                BookedRoomController _bm = new BookedRoomController();
                IEnumerable<BookedRoom> VBR = _bm.GetAllBookedRoom(branchId);

                List<SelectListItem> RoomTypeitems = new List<SelectListItem>();
                RoomTypeitems.Add(new SelectListItem { Text = "Filter by  Room Type", Value = "0" });
                foreach (BookedRoom item in VBR)
                {
                    RoomTypeitems.Add(new SelectListItem { Text = item.RoomTypeName, Value = item.RoomTypeId.ToString()});
                }
                ViewBag.RTComboModel = RoomTypeitems;
                ViewBag.pSize = new List<SelectListItem>()
                    {
                        new SelectListItem() { Value="2", Text= "2" },
                        new SelectListItem() { Value="5", Text= "5" },
                        new SelectListItem() { Value="10", Text= "10" },
                        new SelectListItem() { Value="15", Text= "15" },
                        new SelectListItem() { Value="20", Text= "20" },
                     };

                IPagedList<BookedRoom> bkslist = VBR.ToPagedList(pageNumber, (int)DefaultPageSize);
                return View(bkslist);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }


        public ActionResult Checkout(int bookingId = 0)
        {
            if (Session.Keys.Count != 0)
            {
               
                int branchId = int.Parse(Session["BranchId"].ToString());
                ViewBag.BranchId = branchId;
               BookingController _bc = new BookingController();
                VM_BookingDetails VMB = _bc.GetBookingDetails(branchId, bookingId);
                
                return View("CheckOut", VMB);
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult Guests(int? page, int? pSize = 2)
        {
            if (Session.Keys.Count > 0)
            {
                int? DefaultPageSize = 10;
                if (pSize != null)
                {
                    DefaultPageSize = pSize;
                }

                int pageNumber = page ?? 1;

                int branchId = int.Parse(Session["BranchId"].ToString());

                GuestsController _bm = new GuestsController();
                IEnumerable<Guests> GS = _bm.GetAllGuests(branchId);

                
                
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
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult AddGuest(int GuestId = 0)
        {
            if (Session.Keys.Count != 0)
            {
                //bookingId = 2;
                int branchId = int.Parse(Session["BranchId"].ToString());
               
               GuestsController _pg = new GuestsController();
                Guests bk = new Guests();

                if (GuestId > 0)
                {
                    bk = _pg.GetGuest(branchId,GuestId);

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
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult SaveGuest(Guests guestEntity)
        {

            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                guestEntity.BranchId = branchId;
                GuestsController _bk = new GuestsController();
                bool rtn= _bk.AddGuest(guestEntity);
                return RedirectToAction("Guests");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult MakeVIP(int GuestId)
        {

            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
              
                GuestsController _bk = new GuestsController();
                _bk.SetAsVip(branchId,GuestId);
                return RedirectToAction("Guests");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }

        public ActionResult DelGuest(int GuestId)
        {

            if (Session.Keys.Count != 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());

                GuestsController _bk = new GuestsController();
                _bk.DeleteGuest(branchId, GuestId);
                return RedirectToAction("Guests");
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }
        }
        public ActionResult SaveBookingDocuments()
        {
            if (Session.Keys.Count > 0)
            {
                int branchId = int.Parse(Session["BranchId"].ToString());
                DocumentController _dc = new DocumentController();
                int BookingId = int.Parse(Request.Form["BookingId"].ToString());
                string BookingRef = Request.Form["BookingRef1"].ToString();
                if (Request.Files.Count > 0)
                {
                    
                    for (int i=0;i< Request.Files.Count; i++)
                    {
                        BookingDocuments bd = new BookingDocuments();
                        string _DocTypeid = "DocSelType" + i.ToString();
                        string _DocTypeName = "DocTypeName" + i.ToString();
                        string _PaxId= "Pax"+ i.ToString();


                        string DocTypeId = Request.Form[_DocTypeid].ToString();
                        string DocTypeName= Request.Form[_DocTypeName].ToString();
                        string PaxName= Request.Form[_PaxId].ToString();
                        string fileName = "DocFile" + i.ToString();
                        HttpPostedFileBase postedFile = Request.Files[fileName];
                        byte[] bytes;
                        using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                        {
                            bytes = br.ReadBytes(postedFile.ContentLength);
                        }
                        bd.DocumentName = DocTypeName;
                        bd.DocumentData = bytes;
                        bd.BookingId = BookingId;
                        bd.DocumentTypeId = int.Parse(DocTypeId);
                        bd.PaxName = PaxName;
                        bd.isActive = true;
                       _dc.AddDocuments(bd);
                    }
                }
               
                Session["BranchId"] = branchId;
                return RedirectToAction("BookingProcess", new { BookingId= BookingId, BookingRef= BookingRef });
            }
            else
            {
                return RedirectToAction("index", "unProHome");
            }

        }
    }
}
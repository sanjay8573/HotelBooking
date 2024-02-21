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
                //bookingId = 2;
                int branchId = int.Parse(Session["BranchId"].ToString());
                RoomTypeController RTC = new RoomTypeController();
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

            ViewBag.bkStatus = from bk in bkStatus
                               select new SelectListItem
                                     {
                                         Text = bk.ToString(),
                                         Value = bk.ToString()
                                     };

            ViewBag.pmtStatus = from bk in pmtStatus
                                select new SelectListItem
                               {
                                   Text = bk.ToString(),
                                   Value = bk.ToString()
                               };

            BookingController _bc = new BookingController();
            VM_BookingDetails VMB = _bc.GetBookingDetails(branchId, BookingId);



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
    }
}
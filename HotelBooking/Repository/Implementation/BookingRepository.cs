﻿using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;

namespace HotelBooking.Repository.Implementation
{
    public class BookingRepository : IBooking
    {
        private readonly CompanyContext _context;

        public BookingRepository(CompanyContext context)
        {
            _context = context;

        }
        public BookingRepository()
        {
            _context = new CompanyContext();

        }
        public bool AddAdditionalNight(BookingCost bookingCostEntity)
        {
            bool rtnVal = false;
            if (bookingCostEntity.BookingCostId > 0)
            {
                var tmpEntity = _context.BookingCost.Find(bookingCostEntity.BookingCostId);
                if (tmpEntity != null)
                {
                    
                    tmpEntity.BookingId = bookingCostEntity.BookingId;
                    tmpEntity.RoomTypeId = bookingCostEntity.RoomTypeId;
                    tmpEntity.Date = bookingCostEntity.Date;
                    tmpEntity.CostCategory= bookingCostEntity.CostCategory;
                    tmpEntity.PerNightCost = bookingCostEntity.PerNightCost;
                    tmpEntity.Tax = bookingCostEntity.Tax;
                    tmpEntity.TaxAmount = bookingCostEntity.TaxAmount;
                    

                    _context.SaveChanges();

                }

            }
            else
            {
                try
                {
                    _context.BookingCost.Add(bookingCostEntity);
                    _context.SaveChanges();
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    return false;
                }

            }


            return rtnVal;
        }
        public bool AddAllNights(BookingCost[] bookingCostEntity)
        {
            bool rtnVal = false;
            foreach(BookingCost bc in bookingCostEntity)
           
            {
                rtnVal= AddAdditionalNight(bc);
            }
            return rtnVal;
        }

        public bool AddBooking(BookingRequest bookingRequestEntity)
        {
            int BookingId = 0;
            Booking bookingEntity = new Booking
            {
                BookingId = bookingRequestEntity.BookingId,
                BookingDate = DateTime.Now,
                BookingNumber= bookingRequestEntity.BookingNumber,
                GuestId = bookingRequestEntity.GuestId,
                GuestName = bookingRequestEntity.GuestName,
                BookingTypeId = bookingRequestEntity.BookingTypeId,
                BookingTypeName = bookingRequestEntity.BookingTypeName,
                RoomTypeId = bookingRequestEntity.RoomTypeId,
                RoomTypeName = bookingRequestEntity.RoomTypeName,
                Adult = bookingRequestEntity.Adult,
                Child = bookingRequestEntity.Child,
                ChildAge1= bookingRequestEntity.ChildAge1,
                ChildAge2= bookingRequestEntity.ChildAge2,
                ChildAge3= bookingRequestEntity.ChildAge3,
                CheckIn = bookingRequestEntity.CheckIn,
                Checkout = bookingRequestEntity.Checkout,
                NoOfRooms = bookingRequestEntity.NoOfRooms,
                Nights = bookingRequestEntity.Nights,
                TotalAmount = bookingRequestEntity.TotalAmount,
                TotalTax = bookingRequestEntity.TotalTax,
                PayableAmount = bookingRequestEntity.PayableAmount,
                CouponCode = bookingRequestEntity.CouponCode,
                CouponAmount = bookingRequestEntity.CouponAmount,
                PaidServices = bookingRequestEntity.PaidServices,
                BookingStatus = "New",
                PaymentStatus="Pending",
                BranchId = bookingRequestEntity.BranchId
            };
            bool rtnVal = false;
            if (bookingEntity.BookingId > 0)
            {
                var tmpEntity = _context.Booking.Find(bookingEntity.BookingId);
                if (tmpEntity != null)
                {
                    tmpEntity.BookingNumber = bookingEntity.BookingNumber;
                    tmpEntity.BookingTypeName=bookingEntity.BookingTypeName;
                    tmpEntity.RoomTypeId = bookingEntity.RoomTypeId;
                    tmpEntity.GuestId = bookingEntity.GuestId;
                    tmpEntity.GuestName = bookingEntity.GuestName;
                    tmpEntity.BookingTypeId = bookingEntity.BookingTypeId;
                    tmpEntity.BookingTypeName = bookingEntity.BookingTypeName;
                    tmpEntity.Child = bookingEntity.Child;
                    tmpEntity.Adult = bookingEntity.Adult;
                    tmpEntity.CheckIn = bookingEntity.CheckIn;
                    tmpEntity.Checkout = bookingEntity.Checkout;
                    tmpEntity.NoOfRooms = bookingEntity.NoOfRooms;
                    tmpEntity.Nights = bookingEntity.Nights;
                    tmpEntity.TotalAmount = bookingEntity.TotalAmount;
                    tmpEntity.TotalTax = bookingEntity.TotalTax;
                    tmpEntity.PayableAmount = bookingRequestEntity.PayableAmount;
                    tmpEntity.CouponCode = bookingRequestEntity.CouponCode;
                    tmpEntity.CouponAmount = bookingRequestEntity.CouponAmount;
                    tmpEntity.PaidServices = bookingRequestEntity.PaidServices;
                    tmpEntity.BookingStatus = "New";
                    tmpEntity.PaymentStatus = "Pending";
                    _context.SaveChanges();
                    BookingId = tmpEntity.BookingId;

                }

            }
            else
            {
                try
                {
                    bookingEntity.BookingNumber = gererateBookingNumber();
                    _context.Booking.Add(bookingEntity);
                    _context.SaveChanges();
                    BookingId = bookingEntity.BookingId;
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    return false;
                }

            }
            bool rtnBKC;
            List<BookingCost> allBookingCost = new List<BookingCost>(); 
            foreach (BookingCost bc in bookingRequestEntity.AllNights)
            {
                bc.BookingId = BookingId;
                
                rtnBKC = AddAdditionalNight(bc);
               

            }
            //List<BookingCost> allBookingCostPS = new List<BookingCost>();
            //if (bookingEntity.PaidServices !=null && bookingEntity.PaidServices.Length > 0)
            //{
               
            //    IPaidServices IPS = new PaidServicesRepository();
            //    IEnumerable<PaidServices> psList = IPS.GetPaidServicesByIds(bookingEntity.PaidServices);
            //    foreach( PaidServices p in psList)
            //    {
            //        BookingCost b = new BookingCost
            //        {
            //            BookingId = BookingId,
            //            CostCategory = 2,
            //            PerNightCost = p.Price,
            //            Description=p.Title,
            //            Date = DateTime.Today.Date.ToString(),
            //            RoomTypeId = p.PaidServiceId,
                   
            //        };
            //        rtnBKC = AddAdditionalNight(b);

            //    }

            //}

            return rtnVal;
        }

        public void DeleteBooking(int bookingid)
        {
            try
            {
                var tmpEntity = _context.Booking.Find(bookingid);
                if (tmpEntity != null)
                {
                    _context.Booking.Remove(tmpEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Booking> GetAllBooking(int BranchId)
        {
            IEnumerable<Booking> tempBooking = _context.Booking.Where(b => b.BranchId == BranchId).ToArray();
           foreach(Booking b in tempBooking)
            {
                b.BookingDateTime=b.BookingDate.ToString("MM/dd/yyyy hh:mm tt");
                b.AllNights = GetAllBookingsCost(b.BookingId).ToList();
            }

            return tempBooking;
        }
        public Booking GetBooking(int BookingId)
        {
            Booking tempBooking = new Booking();
             tempBooking = _context.Booking.Where(b => b.BookingId == BookingId).SingleOrDefault<Booking>();

            tempBooking.AllNights = GetAllBookingsCost(tempBooking.BookingId).ToList();
           

            return tempBooking;
        }

        public  void DeleteNights(int bookingid)
        {
            try
            {
                BookingCost tmpEntity = _context.BookingCost.Find(bookingid);
                if (tmpEntity != null)
                {
                    _context.BookingCost.Remove(tmpEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void DeleteNight(int bookingCostId)
        {
            try
            {
                var tmpEntity = _context.BookingCost.Find(bookingCostId);
                if (tmpEntity != null)
                {
                    _context.BookingCost.Remove(tmpEntity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<BookingCost> GetAllBookingsCost(int bookingid) {
            return _context.BookingCost.Where(b => b.BookingId == bookingid).ToArray();
        }
        public IEnumerable<PriceResponse> GetPricesForNight(PriceRequest req)
        {
            List<DateTime> allDates = new List<DateTime>();
            int yy = int.Parse(req.CheckInDate.Substring(6, 4));
            int dd = int.Parse(req.CheckInDate.Substring(0, 2));
            int mm = int.Parse(req.CheckInDate.Substring(3, 2));

            int yy1 = int.Parse(req.CheckOutDate.Substring(6, 4));
            int dd1 = int.Parse(req.CheckOutDate.Substring(0, 2));
            int mm1 = int.Parse(req.CheckOutDate.Substring(3, 2));

            DateTime startDate = new DateTime(yy,mm,dd);
            DateTime endDate = new DateTime(yy1, mm1, dd1);

            for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
            {
                allDates.Add(date.Date);
            }
            List<PriceResponse> ListpResp = new List<PriceResponse>();
            PriceManager PM = _context.PriceManager.Where(c => c.RoomTypeId == req.roomTypeId).FirstOrDefault<PriceManager>();
            
            foreach (DateTime t in allDates)
            {
                decimal spclAmount = GetSpecialRate(t, PM.RoomTypeId);
                if ("MON" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    

                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.MON * 18 / 100),
                        Amount = spclAmount > 0? spclAmount : PM.MON,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.MON,
                        BookingCostId = req.nOfRoom,
                        CostId=PM.PriceManageId,
                        Description=PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);

                }
                if ("TUE" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {

                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.TUE * 18 / 100),
                        Amount = spclAmount > 0 ? spclAmount : PM.TUE,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.TUE,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);

                }
                if ("WED" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.WED * 18 / 100),
                        Amount = spclAmount > 0 ? spclAmount : PM.WED,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.WED,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);

                }
                if ("THU" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.THUR * 18 / 100),
                        Amount = spclAmount > 0 ? spclAmount : PM.THUR,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.THUR,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);
                }
                if ("FRI" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.FRI * 18 / 100),
                        Amount = spclAmount > 0 ? spclAmount : PM.FRI,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.FRI,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);
                }
                if ("SAT" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.SAT * 18 / 100),
                        Amount = spclAmount > 0 ? spclAmount : PM.SAT,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.SAT,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);
                }
                if ("SUN" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = (PM.SUN * 18 / 100),
                        Amount = spclAmount > 0 ? spclAmount : PM.SUN,
                        OfferPrice = spclAmount > 0 ? spclAmount : PM.SUN,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle
                    };
                    ListpResp.Add(p);
                }
                


            }

            return ListpResp;
        }

        private decimal GetSpecialRate(DateTime rDate,int RoomTypeId)
        {
            decimal rtnVal=0;
            SPM sPM = _context.SpecialPrice.Where(c => c.RoomTypeId1 == RoomTypeId && c.isActive1==true).SingleOrDefault<SPM>();
            if (sPM != null)
            {
                int sDay = int.Parse(sPM.DateRangeFrom.Substring(8, 2));
                int sMonth = int.Parse(sPM.DateRangeFrom.Substring(5, 2));
                int sYear = int.Parse(sPM.DateRangeFrom.Substring(0, 4));
                int eDay = int.Parse(sPM.DateRangeTo.Substring(8, 2));
                int eMonth = int.Parse(sPM.DateRangeTo.Substring(5, 2));
                int eYear = int.Parse(sPM.DateRangeTo.Substring(0, 4));

                DateTime from = new DateTime(sYear, sMonth, sDay);
                DateTime to = new DateTime(eYear, eMonth, eDay);

                DateTime t = new DateTime(rDate.Year, rDate.Month, rDate.Day);
                bool isFromDateOK = from <= t && t <= to;
                Console.WriteLine(from <= t && t <= to); // False
                t = new DateTime(rDate.Year, rDate.Month, rDate.Day);
                bool isToOK = from <= t && t <= to;
                Console.WriteLine(from <= t && t <= to); // True

                if (isFromDateOK && isToOK)
                {
                    if ("MON" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {

                        rtnVal = sPM.MON1;

                    }
                    if ("TUE" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.TUE1;

                    }
                    if ("WED" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {

                        rtnVal = sPM.WED1;
                    }
                    if ("THU" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.THUR1;
                    }
                    if ("FRI" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.FRI1;
                    }
                    if ("SAT" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.SAT1;
                    }
                    if ("SUN" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.SUN1;
                    }
                }
            }
            return rtnVal;
        }

        private string gererateBookingNumber()
        {
            Random res = new Random();

            String str = "abcdefghijklmnopqrstuvwxyz0123456789";
            int size = 8;

            // Initializing the empty string 
            String randomstring = "";

            for (int i = 0; i < size; i++)
            {

                // Selecting a index randomly 
                int x = res.Next(str.Length);

                // Appending the character at the  
                // index to the random alphanumeric string. 
                randomstring = randomstring + str[x];
            }
            return randomstring.ToUpper();
        }

        public IEnumerable<Room> GetAllRooms(int roomTypeId)
        {
            List<Room> result= new List<Room>();
            IEnumerable<Room>  ar=_context.Rooms.Where(r => r.RoomTypeId == roomTypeId).ToArray();
           foreach(Room r in ar) {
                if (isRoomAvailable(r.RoomNumber, r.RoomTypeId))
                {
                    result.Add(r); 

                }
            }

            return result.ToArray();
        }

        public bool AddBookedRoom(BookedRoom BookedRoomEntity)
        {
            IBookedRoom IB = new BookedRoomRepository(_context);
            bool rtnVal ;

            rtnVal=IB.AddBookedRoom(BookedRoomEntity);
            
            return rtnVal;
        }

        private bool isRoomAvailable(int  RoomNumber,int roomTypeId)
        {
            bool rtnVal=false;
           BookedRoom BR = _context.BookedRoom.Where(b => b.RoomTypeId == roomTypeId && b.isCheckout == false && b.RoomNumber== RoomNumber.ToString()).SingleOrDefault<BookedRoom>();
            if (BR != null)
            {
                rtnVal = false;
            }
            else
            {
                rtnVal = true;
            }
            return rtnVal;
        }

        System.Collections.Generic.IEnumerable<Booking> IBooking.GetAllBooking(int BranchId)
        {
            return _context.Booking.Where(b => b.BranchId == BranchId).ToArray();
        }

       IEnumerable<BookingCost> IBooking.GetAllBookingsCost(int bookingid)
        {
            return _context.BookingCost.Where(c => c.BookingId == bookingid).ToArray();
        }

        public bool AddBookingPayment(BookingPayments bkpEntity)
        {
            bkpEntity.InvoiceNumber = generateInvoiceNumber(bkpEntity.BranchId);
            bkpEntity.OrderNumber = generateOrderNumber(bkpEntity.BranchId);
            _context.BookingPayments.Add(bkpEntity);
            _context.SaveChanges();
            return true;
        }
        public IEnumerable<BookingPayments> GetAllBookingPayments(int BranchId, int BookingId)
        {
            return _context.BookingPayments.Where(b => b.BranchId == BranchId && b.BookingId == BookingId).ToArray();

        }
        public  BookingPayments GetBookingPayment(int BookingPaymentId)
        {
           return  _context.BookingPayments.Where(b => b.BookingPaymentId == BookingPaymentId).SingleOrDefault();
        }
        public void DeleteBookingPayment(int BookingPaymentId)
        {
            try
            {
                var tmpEntity = _context.BookingPayments.Find(BookingPaymentId);
                if (tmpEntity != null)
                {
                    tmpEntity.isDeleted = true;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public VM_BookingDetails GetBookingDetails(int BranchId, int BookingId)
        {
            VM_BookingDetails VMB = new VM_BookingDetails();

            // get booking details
            Booking b = GetBooking(BookingId);
            VMB.BookingRef = b.BookingNumber;
            VMB.BookingDate = b.BookingDateTime;
            VMB.BookingId = b.BookingId;
            VMB.CheckIn = b.CheckIn;
            VMB.CheckOut = b.Checkout;
            VMB.BookingStatus = b.BookingStatus;
            VMB.PaymnentStatus = b.PaymentStatus;
            VMB.Audlts = b.Adult.ToString();
            VMB.Child = b.Child.ToString();
            VMB.Nights = b.Nights.ToString();
            VMB.Room = b.RoomTypeName;
            VMB.RoomTypeId = b.RoomTypeId;
            VMB.GuestId = b.GuestId;
            VMB.GuestName = b.GuestName;
            VMB.NoOfRooms = b.NoOfRooms;
            //Branch
            Branch br = _context.Branch.Where(br1 => br1.Id == BranchId).SingleOrDefault();
            //Compaany
            Company c = _context.Company.Where(cm => cm.Id == br.CompanyId).SingleOrDefault();
            VMB.CompanyName = c.Name;
            VMB.CompanyAddress = br.Address;
            VMB.CompanyPhone = br.Phone1;
            VMB.CompanyEmail = br.EmailID;

           
            IEnumerable<BookingCost> _PR = GetAllBookingsCost(VMB.BookingId);
            decimal RoomCost = _PR.Select(t => t.PerNightCost).Sum();
            VMB.BookedNiths = _PR;
            IEnumerable<BookingPayments> _BP = GetAllBookingPayments(br.Id, b.BookingId);

            decimal PaidAmount = _BP.Select(t => t.paymentAmount).Sum();
            VMB.TotalPrice = b.TotalAmount;
            VMB.Amountpaid = PaidAmount;
            VMB.AmountPending = (b.TotalAmount - PaidAmount);


            return VMB;
        }


        public bool UpdateBookingStatus(int BranchId,int Bookingid, string bookingStatus)
        {
            Booking bk = _context.Booking.Find(Bookingid);
            bk.BookingStatus = bookingStatus.ToUpper();
            _context.SaveChanges();
            return true;
        }
        public bool UpdatePaymentStatus(int BranchId, int Bookingid, string paymentStatus)
        {
            Booking bk = _context.Booking.Find(Bookingid);
            bk.PaymentStatus = paymentStatus.ToUpper();
            _context.SaveChanges();
            return true;
        }

        public DashBoardData DashboardData(int BranchId)
        {
            DashBoardData ds = new DashBoardData();
            ds.NoOfBooking = _context.Booking.Where(b=>b.BranchId== BranchId).Count();
            ds.NoOfRooms = _context.Rooms.Where(b => b.BranchId == BranchId).Count();
            ds.NoOfGuest = _context.Guests.Where(b => b.BranchId == BranchId).Count();
            
            return ds;
        }

        private string generateInvoiceNumber(int branchId)
        {
            string rtnStr = string.Empty;

            Branch brdata= _context.Branch.Where(b=>b.Id== branchId).SingleOrDefault();
            int lastPaymentId = _context.BookingPayments.Where(b=>b.BranchId== branchId).Select(p => p.BookingPaymentId).Max()+1;

            rtnStr = "INV"+brdata.BranchName.Substring(0, 2) + brdata.Id.ToString() + lastPaymentId.ToString();

            return rtnStr;
        }
        private string generateOrderNumber(int branchId)
        {
            string rtnStr = string.Empty;

            Branch brdata = _context.Branch.Where(b => b.Id == branchId).SingleOrDefault();
            int lastPaymentId = _context.BookingPayments.Where(b => b.BranchId == branchId).Select(p => p.BookingPaymentId).Max() + 1;

            rtnStr = "ORD"+brdata.BranchName.Substring(0, 2) + brdata.Id.ToString() + lastPaymentId.ToString();

            return rtnStr;
        }
    }
    
}

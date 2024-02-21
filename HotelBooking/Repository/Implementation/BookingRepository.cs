using HotelBooking.Context;
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
                    tmpEntity.Date = bookingCostEntity.Date;
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
                rtnBKC= AddAdditionalNight(bc);
               

            }
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
                if ("MON" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {

                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = 0.18M,
                        Date = t.Date.ToString("d"),
                        Day = t.DayOfWeek.ToString(),
                        Amount = PM.MON
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
                        Amount = PM.TUE
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
                        Amount = PM.WED
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
                        Amount = PM.THUR
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
                        Amount = PM.FRI
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
                        Amount = PM.SAT
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
                        Amount = PM.SUN
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
            string[] dates = sPM.DateRange.Split('~') ;
            int sDay = int.Parse(dates[0].Substring(3, 2));
            int sMonth = int.Parse(dates[0].Substring(0, 2));
            int sYear = int.Parse(dates[0].Substring(6, 4));
            int eDay = int.Parse(dates[0].Substring(3, 2));
            int eMonth = int.Parse(dates[0].Substring(0, 2));
            int eYear = int.Parse(dates[0].Substring(6, 4));

            DateTime from = new DateTime(sYear,sMonth,sDay);
            DateTime to = new DateTime(eYear,eMonth,eDay);
            
            DateTime input = new DateTime(rDate.Year, rDate.Month, rDate.Day);
            Console.WriteLine(from <= input && input <= to); // False
            input = new DateTime(rDate.Year,rDate.Month, rDate.Day);
            Console.WriteLine(from <= input && input <= to); // True
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

        System.Collections.Generic.IEnumerable<BookingCost> IBooking.GetAllBookingsCost(int bookingid)
        {
            throw new System.NotImplementedException();
        }

        public bool AddBookingPayment(BookingPayments bkpEntity)
        {
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
            //Branch
            Branch br = _context.Branch.Where(br1 => br1.Id == BranchId).SingleOrDefault();
            //Compaany
            Company c = _context.Company.Where(cm => cm.Id == br.CompanyId).SingleOrDefault();
            VMB.CompanyName = c.Name;
            VMB.CompanyAddress = br.Address;
            VMB.CompanyPhone = br.Phone1;
            VMB.CompanyEmail = br.EmailID;

            PriceRequest pr = new PriceRequest();
            pr.CheckInDate = b.CheckIn;
            pr.CheckOutDate = b.Checkout;
            pr.roomTypeId = b.RoomTypeId;
            IEnumerable<PriceResponse> _PR = GetPricesForNight(pr);
            decimal RoomCost = _PR.Select(t => t.Amount).Sum();
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
    }
    
}

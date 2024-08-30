using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.DynamicPrice;
using HotelBooking.Model.Reatraurant;
using HotelBooking.Model.Report;
using HotelBooking.Model.Tour;
using HotelBooking.Repository.Interface;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml;
using static iTextSharp.text.pdf.AcroFields;

namespace HotelBooking.Repository.Implementation
{
    public class BookingRepository : IBooking
    {
        private readonly CompanyContext _context;

        //public BookingRepository(CompanyContext context)
        //{
        //    _context = context;

        //}
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

        public string AddBooking(BookingRequest bookingRequestEntity)
        {
            int BookingId = 0;
            string BookingNumber = string.Empty;
            Booking bookingEntity = new Booking
            {
                BookingId = bookingRequestEntity.BookingId,
                BookingDate = DateTime.Now,
                BookingNumber = bookingRequestEntity.BookingNumber,
                GuestId = bookingRequestEntity.GuestId,
                GuestName = bookingRequestEntity.GuestName,
                BookingTypeId = bookingRequestEntity.BookingTypeId,
                BookingTypeName = bookingRequestEntity.BookingTypeName,
                RoomTypeId = bookingRequestEntity.RoomTypeId,
                RoomTypeName = bookingRequestEntity.RoomTypeName,
                Adult = bookingRequestEntity.Adult,
                Child = bookingRequestEntity.Child,
                ChildAge1 = bookingRequestEntity.ChildAge1,
                ChildAge2 = bookingRequestEntity.ChildAge2,
                ChildAge3 = bookingRequestEntity.ChildAge3,
                CheckIn = bookingRequestEntity.CheckIn,
                Checkout = bookingRequestEntity.Checkout,
                NoOfRooms = bookingRequestEntity.NoOfRooms,
                Nights = bookingRequestEntity.Nights,
                TotalAmount = bookingRequestEntity.TotalAmount,
                TotalTax = bookingRequestEntity.TotalTax,
                PayableAmount = bookingRequestEntity.PayableAmount,
                BookingSourceId = bookingRequestEntity.BookingSourceId,
                CommissionPaid = bookingRequestEntity.CommissionPaid,
                CouponCode = bookingRequestEntity.CouponCode,
                CouponAmount = bookingRequestEntity.CouponAmount,
                PaidServices = bookingRequestEntity.PaidServices,
                BookingStatus = "HN",
                BookingChannel = "BO-DC",
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
                    tmpEntity.ChildAge1 = bookingEntity.ChildAge1;
                    tmpEntity.ChildAge2 = bookingEntity.ChildAge2;
                    tmpEntity.ChildAge3 = bookingEntity.ChildAge3;
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
                    BookingNumber = bookingEntity.BookingNumber;
                    rtnVal = true;
                }
                catch (Exception ex)
                {

                    rtnVal = false;
                }

            }
            bool rtnBKC;
            List<BookingCost> allBookingCost = new List<BookingCost>(); 
            foreach (BookingCost bc in bookingRequestEntity.AllNights)
            {
                bc.BookingId = BookingId;
                
                rtnBKC = AddAdditionalNight(bc);
               

            }
           

            return BookingNumber;
        }
        public string AddOnlineBooking(BookingRequest bookingRequestEntity)
        {
            int BookingId = 0;
            string BookingNumber = string.Empty;
            Booking bookingEntity = new Booking
            {
                BookingId = bookingRequestEntity.BookingId,
                BookingDate = DateTime.Now,
                BookingNumber = bookingRequestEntity.BookingNumber,
                GuestId = CheckExistingGuest(bookingRequestEntity.GuestContactNumber, bookingRequestEntity.GuestEmail, bookingRequestEntity.GuestFirstName, bookingRequestEntity.GuestLastName, bookingRequestEntity.BranchId),
                GuestName = bookingRequestEntity.GuestFirstName +" "+ bookingRequestEntity.GuestLastName,
                BookingTypeId = bookingRequestEntity.BookingTypeId,
                BookingTypeName = bookingRequestEntity.BookingTypeName,
                RoomTypeId = bookingRequestEntity.RoomTypeId,
                RoomTypeName = bookingRequestEntity.RoomTypeName,
                Adult = bookingRequestEntity.Adult,
                Child = bookingRequestEntity.Child,
                ChildAge1 = bookingRequestEntity.ChildAge1,
                ChildAge2 = bookingRequestEntity.ChildAge2,
                ChildAge3 = bookingRequestEntity.ChildAge3,
                CheckIn = bookingRequestEntity.CheckIn,
                Checkout = bookingRequestEntity.Checkout,
                NoOfRooms = bookingRequestEntity.NoOfRooms,
                Nights = bookingRequestEntity.Nights,
                TotalAmount = bookingRequestEntity.TotalAmount,
                TotalTax = bookingRequestEntity.TotalTax,
                PayableAmount = bookingRequestEntity.PayableAmount,
                BookingSourceId = bookingRequestEntity.BookingSourceId,
                CommissionPaid = bookingRequestEntity.CommissionPaid,
                CouponCode = bookingRequestEntity.CouponCode,
                CouponAmount = bookingRequestEntity.CouponAmount,
                PaidServices = bookingRequestEntity.PaidServices,
                BookingChannel = bookingRequestEntity.BookingChannel,
                BookingStatus = "New",
                PaymentStatus = "Pending",
                BranchId = bookingRequestEntity.BranchId
            };
           
            
                try
                {
                    bookingEntity.BookingNumber = gererateBookingNumber();
                    _context.Booking.Add(bookingEntity);
                    _context.SaveChanges();
                    BookingId = bookingEntity.BookingId;
                    BookingNumber = bookingEntity.BookingNumber;
                    
                }
                catch (Exception ex)
                {

                    return "Opps!! Smething went wrong...";
                }

            bool rtnBKC;
            List<BookingCost> allBookingCost = new List<BookingCost>();
            foreach (BookingCost bc in bookingRequestEntity.AllNights)
            {
                bc.BookingId = BookingId;

                rtnBKC = AddAdditionalNight(bc);


            }


            return BookingNumber;
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
            
            for (DateTime date = req.CheckInDate; date < req.CheckOutDate; date = date.AddDays(1))
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

                    decimal amt = (spclAmount > 0 ? spclAmount : PM.MON);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());                   
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType=="P")? amt* taxVal / 100: amt+ taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        
                       
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = _TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId=PM.PriceManageId,
                        Description=PM.RoomTypeTitle,
                        isAvailable=true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);

                }
                if ("TUE" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    decimal amt = (spclAmount > 0 ? spclAmount : PM.TUE);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType == "P") ? amt * taxVal / 100 : amt + taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = _TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle,
                        isAvailable = true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);

                }
                if ("WED" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    decimal amt = (spclAmount > 0 ? spclAmount : PM.WED);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType == "P") ? amt * taxVal / 100 : amt + taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = _TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle,
                        isAvailable = true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);

                }
                if ("THU" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    decimal amt = (spclAmount > 0 ? spclAmount : PM.THUR);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType == "P") ? amt * taxVal / 100 : amt + taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = _TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle,
                        isAvailable = true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);
                }
                if ("FRI" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    decimal amt = (spclAmount > 0 ? spclAmount : PM.FRI);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType == "P") ? amt * taxVal / 100 :  taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount =_TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle,
                        isAvailable = true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);
                }
                if ("SAT" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    decimal amt = (spclAmount > 0 ? spclAmount : PM.SAT);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType == "P") ? amt * taxVal / 100 : amt + taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = _TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle,
                        isAvailable = true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);
                }
                if ("SUN" == t.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                {
                    decimal amt = (spclAmount > 0 ? spclAmount : PM.SUN);
                    amt = getDynamicPrice(t, amt, PM.RoomTypeId, req.BranchId);
                    TaxMaster _tm = getConfiguredTax("ROOMS", req.BranchId, double.Parse(amt.ToString()));
                    decimal taxVal = Decimal.Parse(_tm.Value.ToString());
                    string TextType = _tm.TaxType;
                    decimal _TaxAmount = (TextType == "P") ? amt * taxVal / 100 : amt + taxVal;
                    PriceResponse p = new PriceResponse
                    {
                        roomTypeId = PM.RoomTypeId,
                        Tax = taxVal,
                        Date = t.Date,
                        Day = t.DayOfWeek.ToString(),
                        TaxAmount = _TaxAmount,
                        Amount = amt,
                        OfferPrice = amt,
                        BookingCostId = req.nOfRoom,
                        CostId = PM.PriceManageId,
                        Description = PM.RoomTypeTitle,
                        isAvailable = true
                    };
                    if (!isRoomTypeAvailable(PM.RoomTypeId, req.BranchId, t))
                    {
                        p.isAvailable = false;
                    }
                    ListpResp.Add(p);
                }
                


            }

            return ListpResp;
        }

        private decimal GetSpecialRate(DateTime rDate,int RoomTypeId)
        {
            decimal rtnVal=0;
            SPM sPM = _context.SpecialPrice.Where(c => c.RoomTypeId1 == RoomTypeId && c.isActive1==true && (rDate>=c.DateRangeFrom && rDate<=c.DateRangeTo)).SingleOrDefault<SPM>();
            if (sPM != null)
            {
                //int sDay = int.Parse(sPM.DateRangeFrom.Substring(8, 2));
                //int sMonth = int.Parse(sPM.DateRangeFrom.Substring(5, 2));
                //int sYear = int.Parse(sPM.DateRangeFrom.Substring(0, 4));
                //int eDay = int.Parse(sPM.DateRangeTo.Substring(8, 2));
                //int eMonth = int.Parse(sPM.DateRangeTo.Substring(5, 2));
                //int eYear = int.Parse(sPM.DateRangeTo.Substring(0, 4));

                //DateTime from = new DateTime(sYear, sMonth, sDay);
                //DateTime to = new DateTime(eYear, eMonth, eDay);

                //DateTime t = new DateTime(rDate.Year, rDate.Month, rDate.Day);
                //bool isFromDateOK = from <= t && t <= to;
                //Console.WriteLine(from <= t && t <= to); // False
                //t = new DateTime(rDate.Year, rDate.Month, rDate.Day);
                //bool isToOK = from <= t && t <= to;
                //Console.WriteLine(from <= t && t <= to); // True

                //if (isFromDateOK && isToOK)
                //{
                    if ("MON" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {

                        rtnVal = sPM.MON1;

                    }
                    if ("TUE" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.TUE1;

                    }
                    if ("WED" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {

                        rtnVal = sPM.WED1;
                    }
                    if ("THU" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.THUR1;
                    }
                    if ("FRI" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.FRI1;
                    }
                    if ("SAT" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.SAT1;
                    }
                    if ("SUN" == rDate.DayOfWeek.ToString().Substring(0, 3).ToUpper())
                    {
                        rtnVal = sPM.SUN1;
                    }
                //}
            }
            return rtnVal;
        }

        public string gererateBookingNumber()
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
            IBookedRoom IB = new BookedRoomRepository();
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

            string rtnInvStr = string.Empty;
            string rtnOrdStr = string.Empty;

            Branch brdata = _context.Branch.Where(b => b.Id == bkpEntity.BranchId).SingleOrDefault();
            DocType dc = _context.DocType.Where(b => b.BranchId == bkpEntity.BranchId && b.isActive == true && b.DocCategory == 1).SingleOrDefault();
            int lastPaymentId = dc.DocNumber + 1;
            dc.DocNumber = lastPaymentId;

            rtnInvStr = "INV" + brdata.BranchName.Substring(0, 2).ToUpper() + brdata.Id.ToString() + lastPaymentId.ToString();
            rtnOrdStr = "ORD" + brdata.BranchName.Substring(0, 2).ToUpper() + brdata.Id.ToString() + lastPaymentId.ToString();
            bkpEntity.InvoiceNumber = rtnInvStr;//generateInvoiceNumber(bkpEntity.BranchId);
            bkpEntity.OrderNumber = rtnOrdStr;//generateOrderNumber(bkpEntity.BranchId);
            bkpEntity.PaymentDate = DateTime.Now;
            _context.BookingPayments.Add(bkpEntity);
            if (bkpEntity.PaymentFor == 2)
            {
                if (bkpEntity.paymentDue <= bkpEntity.paymentAmount)
                {
                    
                    if (bkpEntity.ServiceType == "Dining")
                    {
                        BillingMaster BM = _context.BillingMaster.Where(b => b.RestaurantId == bkpEntity.BookingId && b.Tableid == bkpEntity.Table_Room_Number && b.isPark==true).SingleOrDefault();
                        BM.isPark = false;
                        RestaurantTables rt = _context.RestaurantTables.Find(bkpEntity.Table_Room_Number);
                        rt.isOccupied = false;
                    }
                    if (bkpEntity.ServiceType == "RoomService")
                    { 
                        BillingMaster BM1 = _context.BillingMaster.Where(b => b.RestaurantId == bkpEntity.BookingId && b.TableNo_RoomNumber == bkpEntity.Table_Room_Number && b.isPark==true).SingleOrDefault();
                        BM1.isPark = false;
                        RestaurantRoomService rrs = _context.RestaurantRoomService.Where(b=>b.RoomNumber== bkpEntity.Table_Room_Number.ToString() && b.RestaurantId==bkpEntity.BookingId).SingleOrDefault();
                        rrs.isOrdered = false;
                    }

                }
                
            }
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
            Guests gst=_context.Guests.Where(g=>g.GuestId== b.GuestId).SingleOrDefault();
            VMB.GuestId = gst.GuestId;
            VMB.GuestName = gst.Name;
            VMB.GuestAddress = gst.Address;
            VMB.GuestCity = gst.city;
            VMB.Guestcountry = gst.country;
            VMB.Guestpincode = gst.pincode;
            VMB.GuestPhone = gst.Phone;
            VMB.Guestemail = gst.email;

            VMB.NoOfRooms = b.NoOfRooms;
            //Branch
            Branch br = _context.Branch.Where(br1 => br1.Id == BranchId).SingleOrDefault();
            //Compaany
            Company c = _context.Company.Where(cm => cm.Id == br.CompanyId).SingleOrDefault();
            VMB.CompanyName = br.BranchName;
            VMB.CompanyAddress = br.Address;
            VMB.CompanyPhone = br.Phone1;
            VMB.CompanyEmail = br.EmailID;

           
            IEnumerable<BookingCost> _PR = GetAllBookingsCost(VMB.BookingId).Where(b1=>b1.CostCategory<=2);
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
        public IEnumerable<DashBoardData> CalendarData(int BranchId)
        {
            IEnumerable<DashBoardData> ds= new List<DashBoardData>();
            int totalRooms = _context.Rooms.Where(b => b.BranchId == BranchId).Count();
            var bkedRoom = _context.BookedRoom.Where(b => b.BranchId == BranchId && b.isCheckout == false)
                            .GroupBy(g => new { dt = g.CheckIn })
                            .Select( entity=>
                                    new DashBoardData
                                    {
                                        NoOfRooms= totalRooms,
                                        AvailableRooms = totalRooms- entity.Count(),
                                        BookedRooms =entity.Count(),
                                        AvailabilityDate=entity.Key.dt.ToString()
                                    }
                                ).ToList();

            return bkedRoom;

        }
        public IEnumerable<DashBoardData> CalendarDataNew(int BranchId,int month, int Year)
        {
            string strsql = "exec FSP_GetBookedRoomMonthWise @Branchid=" + BranchId + ",@month=" + month + ",@year=" + Year;
           
            
            IEnumerable<DashBoardData> ds = new List<DashBoardData>();
            ds=_context.Database.SqlQuery<DashBoardData>(strsql).ToList();

            return ds;

        }


        private string generateInvoiceNumber(int branchId)
        {
            string rtnStr = string.Empty;

            Branch brdata= _context.Branch.Where(b=>b.Id== branchId).SingleOrDefault();
            DocType dc = _context.DocType.Where(b => b.BranchId == branchId && b.isActive == true && b.DocCategory == 1).SingleOrDefault();
            int lastPaymentId = dc.DocNumber+1;
            dc.DocNumber = lastPaymentId;
            _context.SaveChanges();
            rtnStr = "INV"+brdata.BranchName.Substring(0, 2) + brdata.Id.ToString() + lastPaymentId.ToString();

            return rtnStr;
        }
        private string generateOrderNumber(int branchId)
        {
            string rtnStr = string.Empty;

            Branch brdata = _context.Branch.Where(b => b.Id == branchId).SingleOrDefault();
            DocType dc = _context.DocType.Where(b => b.BranchId == branchId && b.isActive == true && b.DocCategory == 2).SingleOrDefault();
            int lastPaymentId = dc.DocNumber + 1;
            dc.DocNumber = lastPaymentId;
            _context.SaveChanges();
            rtnStr = "ORD"+brdata.BranchName.Substring(0, 2) + brdata.Id.ToString() + lastPaymentId.ToString();

            return rtnStr;
        }

        private bool isRoomTypeAvailable(int roomTypeId,int branchId,DateTime checkinDate)
        {
            bool rtnVal = true;
            
            List<Booking> extBk = _context.Booking.Where(b =>b.BranchId==branchId && b.RoomTypeId.Contains(roomTypeId.ToString())).ToList();
            foreach(var item in extBk)
            {
                //int yy = int.Parse(item.CheckIn.Substring(6, 4));
                //int dd = int.Parse(item.CheckIn.Substring(0, 2));
                //int mm = int.Parse(item.CheckIn.Substring(3, 2));
                //int yy1 = int.Parse(item.Checkout.Substring(6, 4));
                //int dd1 = int.Parse(item.Checkout.Substring(0, 2));
                //int mm1 = int.Parse(item.Checkout.Substring(3, 2));
                //DateTime bkChkinDateDate = new DateTime(yy, mm, dd);
                // bkChkOutDateDate = new DateTime(yy1, mm1, dd1);
                if(checkinDate>= item.CheckIn && checkinDate<= item.Checkout)
                {
                    rtnVal = false;
                }
            }
            return rtnVal;
        }

        private int CheckExistingGuest(string gContactNumber,string gEmail,string gFirstName,string gLastName,int branchId)
        {
            int rtnGuestId = 0;
            string gName = gFirstName + " " + gLastName;
            Guests gst = _context.Guests.Where(g => g.email == gEmail && g.Phone == gContactNumber && g.Name == gName && g.BranchId== branchId).FirstOrDefault();
            if (gst!=null){
                rtnGuestId = gst.GuestId;
            }
            else
            {
                Guests g = new Guests
                {
                    Name=gName,
                    Phone= gContactNumber,
                    email=gEmail,
                    BranchId=branchId,
                    isActive=true,
                    isDeleted=false,
                    isVIP=false

                };
                _context.Guests.Add(g);
                _context.SaveChanges();
                rtnGuestId = g.GuestId;
            }
            return rtnGuestId;
        }

        public IEnumerable<Tour> GetAllTourBooking(int BranchId)
        {

            IEnumerable<Tour> tempTourBooking = _context.Tours.Where(b => b.BranchId == BranchId).ToArray();
           
            return tempTourBooking;
        }

       private TaxMaster getConfiguredTax(string AppliedFor, int branchId, double _amount)
        {
           TaxMaster allTax = _context.TaxMaster.Where(t => t.BranchId == branchId && t.appliedForName.ToUpper()== AppliedFor.ToUpper() && (_amount>=t.RangeFrom && _amount<=t.RangeTo)).OrderByDescending(o=>o.Value).FirstOrDefault();
            return allTax;
        }
        #region " Dynamic Pricing Checks"
        private decimal getDynamicPrice(DateTime rDate,decimal baseAmount,int roomTypeId,int branchId)
        {
           
             
            IEnumerable<Booking> _bookedRoom = _context.Booking.Where(b => b.BranchId == branchId  && b.RoomTypeId.Contains(roomTypeId.ToString())).ToArray();
            int BookedRoomCount = 0;
            foreach (var item in _bookedRoom)
            {
                //int yy = int.Parse(item.CheckIn.Substring(6, 4));
                //int dd = int.Parse(item.CheckIn.Substring(0, 2));
                //int mm = int.Parse(item.CheckIn.Substring(3, 2));
               //DateTime bkChkinDateDate = new DateTime(yy, mm, dd);
                
                if (rDate == item.CheckIn)
                {
                    BookedRoomCount++;
                }
            }

            int _totalRooms = _context.Rooms.Where(r => r.RoomTypeId == roomTypeId && r.BranchId == branchId).Count();
            int _avlRooms = _totalRooms - BookedRoomCount;
            
            
            DynamicPriceModel _dnp=_context.DynamicPrice.Where(r=>r.RoomTypeId== roomTypeId).FirstOrDefault();
            if (_dnp != null)
            {
                var dnpBase = (_dnp.Slab1_Thresold >= _avlRooms && _dnp.Slab2_Thresold <= _avlRooms) ? _dnp.Slab1 :
                    (_dnp.Slab2_Thresold >= _avlRooms && _dnp.Slab3_Thresold <= _avlRooms) ? _dnp.Slab2 : (_dnp.Slab1_Thresold <= _avlRooms && _dnp.Slab2_Thresold <= _avlRooms) ? _dnp.Slab3 : 0;

                if (_dnp.isFixed)
                {
                    baseAmount = baseAmount + dnpBase;
                }
                else
                {
                    baseAmount = baseAmount + (baseAmount * dnpBase / 100);
                }

            }
            return baseAmount;
        }
        #endregion 
    }

}

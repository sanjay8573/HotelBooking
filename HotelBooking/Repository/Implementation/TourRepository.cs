using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Tour;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class TourRepository : ITour
    {
        private readonly CompanyContext _context;
        public TourRepository()
        {
            _context= new CompanyContext();
        }
        public TourBookingResponse CreatingTourBooking(TourBookingRequest request)
        {
            TourBookingResponse tourBookingResponse = new TourBookingResponse();
            //Step 1. Check Guest if already exist if yes get Guest id
            //STep 2. Save Tour Booking and send above Guest Id and get TourBooking Id
            //Step 3. Save Tour Details and pass above TourBooking Id
            //Step 4. Save Payment details with above tourBokingId
            
            int gstId = 0;
            if (request.TourDetail.BookingId > 0)
            {

                var bk = _context.Booking.Where(b => b.BookingId == request.TourDetail.BookingId && b.BranchId == request.TourDetail.BranchId).FirstOrDefault();
                gstId=bk.GuestId;
            }

            int TourBookingId = 0;
            string BookingNumber = string.Empty;
            Tour _tour = new Tour
            {
                GuestId =(gstId>0 ) ? gstId : CheckExistingGuest(request.TourGuest.Phone, request.TourGuest.email, request.TourGuest.Name, request.TourGuest.Name, request.TourDetail.BranchId),
                BookingDate = DateTime.Now,
                GuestName= request.TourGuest.Name,
                TotalAmount = request.TourDetail.TotalAmount,
                TotalTax = request.TourDetail.TotalTax,
                PayableAmount = request.TourDetail.PayableAmount,
                Discount = request.TourDetail.Discount,
                CouponCode = request.TourDetail.CouponCode,
                CouponAmount = request.TourDetail.CouponAmount,
                BookingId = request.TourDetail.BookingId,
                RoomNumber = request.TourDetail.RoomNumber,
                SelectedServices = request.TourDetail.SelectedServices,
                BookingChannel = request.TourDetail.BookingChannel,
                BookingStatus = request.TourDetail.BookingStatus,
                PaymentStatus = request.TourDetail.PaymentStatus,
                StartDate = request.TourDetail.StartDate,
                EndDate = request.TourDetail.EndDate,
                BranchId = request.TourDetail.BranchId,
                isActive = request.TourDetail.isActive,
                isDeleted = false
            };
            try
            {
                

                _tour.BookingNumber = new BookingRepository().gererateBookingNumber();
                _context.Tours.Add(_tour);
                _context.SaveChanges();
                if (_tour.BookingId > 0)
                {
                    new BookingRepository().UpdateBookingCost(_tour.BookingId,decimal.Parse(_tour.TotalAmount.ToString()), decimal.Parse(_tour.TotalTax.ToString()), decimal.Parse(_tour.TotalAmount.ToString()));
                }
                
                TourBookingId = _tour.TourBookingId;
                BookingNumber = _tour.BookingNumber;
                tourBookingResponse.Error = new Model.onlineAPI.Error { Code = "0000", Description = "No Error || Tour Inserted" };
               
                tourBookingResponse.isSuccess = true;
                tourBookingResponse.BookingRef = BookingNumber;


            }
            catch (Exception ex)
            {
                tourBookingResponse.Error = new Model.onlineAPI.Error { Code = "TRBK001", Description = "Opps!! Smething went wrong..." };


               
            }
            List<BookingCost> allBookingCost = new List<BookingCost>();
            foreach (BookingCost bc in request.TourDetails)
            {
                bc.BookingId = TourBookingId;
                bc.PBookingId = request.TourDetail.BookingId;
               bool rtnBKC = new BookingRepository().AddAdditionalNight(bc);


            }
            tourBookingResponse.Error = new Model.onlineAPI.Error { Code = "0000", Description = "No Error || Tour Details Inserted" };

            tourBookingResponse.isSuccess = true;
            tourBookingResponse.BookingRef = BookingNumber;
            if (request.TourPayment != null)
            {
                request.TourPayment.BookingId = TourBookingId;
                bool rtnPayment = new BookingRepository().AddBookingPayment(request.TourPayment);
                if (rtnPayment)
                {

                    tourBookingResponse.Error = new Model.onlineAPI.Error { Code = "0000", Description = "No Error || Tour Payment Inserted" };

                    tourBookingResponse.isSuccess = true;
                    tourBookingResponse.BookingRef = BookingNumber;

                }
                else
                {
                    tourBookingResponse.Error = new Model.onlineAPI.Error { Code = "TRBK001", Description = "Error while Tour Details Insert" };


                    tourBookingResponse.isSuccess = true;
                    tourBookingResponse.BookingRef = BookingNumber;

                }
            }
            
            
            return tourBookingResponse;
        }
        public bool CancellTourBooking(int tourId)
        {
            bool rtnVal = false;
            try
            {
                Tour t = _context.Tours.Find(tourId);
                if (t != null)
                {
                    t.BookingStatus = "HX";
                    _context.SaveChanges();
                }
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;
        }
        public bool MarkAsFullPayment(int tourId)
        {
            bool rtnVal = false;
            try
            {
                Tour t = _context.Tours.Find(tourId);
                if (t != null)
                {

                    BookingPayments _bkp = new BookingPayments();
                    _bkp.paymentAmount=decimal.Parse(t.TotalAmount.ToString());
                    _bkp.paymentDue = 0;
                    _bkp.BookingId = t.TourBookingId;
                    _bkp.PaymentDate = DateTime.Now;
                    _bkp.BranchId = t.BranchId;
                    _bkp.isActive = true;
                    _bkp.isDeleted = false;
                    _bkp.PaymentType = 4;
                    _bkp.PaymentFor = 4;
                    _bkp.Table_Room_Number = int.Parse(t.RoomNumber);
                    _bkp.ServiceType = "TOUR";
                    _bkp.PaymentTypeName = "CASH";
                    _bkp.Remarks = "Payment Received for Tour";

                    bool rtnPayment = new BookingRepository().AddBookingPayment(_bkp);
                    if (rtnPayment)
                    {
                        t.PaymentStatus = "Complete";
                        t.BookingStatus = "HK";
                        _context.SaveChanges();
                    }
                   
                    
                }
                rtnVal = true;
            }
            catch (Exception)
            {

                rtnVal = false;
            }
            return rtnVal;
        }
        public IEnumerable<Tour> getAllTourBooking(int branchId)
        {
           return  _context.Tours.Where(b => b.BranchId == branchId);
        }

        public TourBookingRequest getTourBooking(int branchId,int TourId)
        {
            TourBookingRequest _rr = new TourBookingRequest();
            _rr.TourDetail = _context.Tours.Where(t => t.TourBookingId == TourId).SingleOrDefault();
            _rr.TourGuest=_context.Guests.Where(t => t.GuestId==_rr.TourDetail.GuestId).SingleOrDefault();
            _rr.TourDetails = _context.BookingCost.Where(b => b.BookingId == TourId && b.CostCategory == 3).ToList();
            _rr.TourPayment=_context.BookingPayments.Where(p=>p.BookingId == TourId && p.ServiceType=="TOUR").SingleOrDefault();
            return _rr;// _context.Tours.Where(b => b.BranchId == branchId);
        }
        private int CheckExistingGuest(string gContactNumber, string gEmail, string gFirstName, string gLastName, int branchId)
        {
            int rtnGuestId = 0;
            string gName = gFirstName + " " + gLastName;
            Guests gst = _context.Guests.Where(g => g.email == gEmail && g.Phone == gContactNumber && g.Name == gName && g.BranchId == branchId).FirstOrDefault();
            if (gst != null)
            {
                rtnGuestId = gst.GuestId;
            }
            else
            {
                Guests g = new Guests
                {
                    Name = gName,
                    Phone = gContactNumber,
                    email = gEmail,
                    BranchId = branchId,
                    isActive = true,
                    isDeleted = false,
                    isVIP = false

                };
                _context.Guests.Add(g);
                _context.SaveChanges();
                rtnGuestId = g.GuestId;
            }
            return rtnGuestId;
        }
    }
}
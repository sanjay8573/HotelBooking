using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Tour
{
    public class TourBookingRequest
    {
        public  object imageFile { get; set; }
        
        public Tour TourDetail { get; set; }
        public Guests TourGuest { get; set; }
        public List<BookingCost> TourDetails { get; set; }
        public BookingPayments TourPayment { get; set; }
    }
}
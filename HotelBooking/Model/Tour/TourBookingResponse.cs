using HotelBooking.Model.onlineAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Tour
{
    public class TourBookingResponse
    {
        public Error Error { get; set; }
        public string BookingRef { get; set; }
        public bool isSuccess { get; set; }
    }
}
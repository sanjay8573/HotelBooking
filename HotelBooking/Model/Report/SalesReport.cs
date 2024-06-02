using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class SalesReport
    {
        public string BookingRef { get; set; }
        public DateTime BookingDate { get; set; }
        public int BookingSourceId { get; set; }
        public string BookingSource { get; set; }
        public int GuestId { get; set; }
        public string GuestName { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public decimal BookingAmount { get; set; }
        public double CommissionPaid { get; set; }
    }

    public class TourSalesReport
    {
        public string BookingRef { get; set; }
        public DateTime BookingDate { get; set; }      
       
        public string GuestName { get; set; }
        public string StartDate { get; set; }
        public string ENDDate { get; set; }
        public decimal BookingAmount { get; set; }
        public string BookingStatus { get; set; }
        public string PaymentStatus { get; set; }

    

    }
}
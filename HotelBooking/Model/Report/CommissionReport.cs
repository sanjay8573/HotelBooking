using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class CommissionReport
    {
        public int BookingSourceId { get; set; }
        public string BookingSource { get; set; }
        public decimal TotalBookingAmount { get; set; }
        public double TotalCommissionPaid { get; set; }

    }
}
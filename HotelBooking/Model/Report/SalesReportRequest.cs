using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class SalesReportRequest
    {
        public int BranchId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int BookingSourceId { get; set; }
        public string BookingSource { get; set; }
        public string bookingRef { get; set; }
    }
    public class TourSalesReportRequest
    {
        public int BranchId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string bookingRef { get; set; }
    }
}
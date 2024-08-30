using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class RestaurantSalesReportRequest
    {
        public int BranchId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int RestaurantId { get; set; }
    }



    public class VM_RestaurantSalesReport
    {
        public RestaurantSalesReportRequest RSR { get; set; }
        public IEnumerable<RestaurantSalesReport> RestaurantSalesReport { get; set; }
    }
}
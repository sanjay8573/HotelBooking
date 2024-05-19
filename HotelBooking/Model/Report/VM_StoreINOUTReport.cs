using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class VM_StoreINOUTReport
    {
        public StoreINOUTReportRequest REQ { get; set; }
        public IEnumerable<StoreINOUTReport> StoreINOUTReport { get; set; }
    }
    public class StoreINOUTReportRequest
    {
        public int BranchId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string Action { get; set; }
        
    }
}
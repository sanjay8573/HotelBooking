using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class VM_SalesReport
    {
        public SalesReportRequest SalesReportRequestData { get; set; }
        public IEnumerable<SalesReport> salesReports { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class VM_CommissionReport
    {
        public CommissionReportRequest CRR { get; set; }
        public IEnumerable<CommissionReport> CommissionReport { get; set; }
    }

    public class CommissionReportRequest
    {
        public int BranchId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int BookingSourceId { get; set; }
        public string  BookingSource { get; set; }

    }
}
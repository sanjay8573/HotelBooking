using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class VM_CurrencyExchangeReport
    {
        public CurrencyExchangeReportRequest REQ { get; set; }
        public IEnumerable<ExchangeTransaction> CurrencyExchangeReport { get; set; }
    }

    public class CurrencyExchangeReportRequest
    {
        public int BranchId { get; set; }
        public int ExchangeCurrencyId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
}
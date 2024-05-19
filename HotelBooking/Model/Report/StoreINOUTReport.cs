using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class StoreINOUTReport
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public string action { get; set; }
        public int BalanceQty { get; set; }
        public DateTime issuedate { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class CurrencyExchangeReport
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double ROE { get; set; }
        public double Total_IN { get; set; }
        public string RoomNumber { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model
{
    public class CurrResp
    {
        
        public int CurrExchangeId { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public double ExchangeValue { get; set; }
    }
}
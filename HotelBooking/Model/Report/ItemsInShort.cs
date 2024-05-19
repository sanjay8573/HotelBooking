using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class ItemsInShort
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CurrentQty { get; set; }
        public int ReservedQty { get; set; }

    }
}
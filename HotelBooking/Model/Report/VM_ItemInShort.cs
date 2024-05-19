using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class VM_ItemInShort
    {
        public ItemInShortRequest REQ { get; set; }
        public IEnumerable<ItemsInShort> itemsInShorts { get; set; }    
    }
    public class ItemInShortRequest
    {
        public int BranchId { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
    }
}
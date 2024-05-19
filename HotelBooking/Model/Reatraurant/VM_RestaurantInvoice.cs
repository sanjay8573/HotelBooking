using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Reatraurant
{
    public class VM_RestaurantInvoice
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchLogo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public  BillingMaster BillDetails { get; set; }

    }
}
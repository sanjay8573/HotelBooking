using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class RestaurantSalesReport
    {
        public int Billingid { get; set; }
        public string RestaurantName { get; set; }
        public string TableName { get; set; }
        public DateTime SalesDate { get; set; }
        public double TotalAmount { get; set; }
        public double TaxAmount { get; set; }
        public double GrantTotal { get; set; }
       
        public string RoomNumber { get; set; }
        public string PaidThrough { get; set; }
        public bool isPark { get; set; }
        public DateTime BillingTime { get; set; }

    }
}
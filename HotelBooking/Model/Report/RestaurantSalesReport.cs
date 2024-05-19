using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Report
{
    public class RestaurantSalesReport
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public DateTime SalesDate { get; set; }
        public double  TotalSales { get; set; }

        public string RoomNumber { get; set; }
        public string PaidThrough { get; set; }

    }
}
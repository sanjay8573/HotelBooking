using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace HotelBooking.Model.Reatraurant
{
    public class VM_MenuHeadings
    {
        public int RestaurantId { get; set; }
        public string  RestaurantName { get; set; }
        public int RestaurantMenuId { get; set; }
        public int MenuHeadingId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuHeadingName { get; set; }
    }
}
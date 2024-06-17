using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotelBooking.Model.Reatraurant
{
    [Table("RestaurantMenu")]
    public class RestaurantMenu
    {
        public RestaurantMenu() {
            MenuHeading = new List<RestaurantMenuHeading>();
        }
        [Key]
        public int RestaurantMenuId { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantMenuName { get; set; }
        [NotMapped]
        public List<RestaurantMenuHeading> MenuHeading { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
    [Table("RestaurantMenuHeading")]
    public class RestaurantMenuHeading
    {
        public RestaurantMenuHeading()
        {
            MenuItems= new List<RestaurantMenuItem>();
        }
        [Key]
        public int MenuHeadingId { get; set; }
        public int RestaurantMenuId { get; set; }
        public string MenuHeadingName { get; set; }
        [NotMapped]
        public List<RestaurantMenuItem> MenuItems { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int TaxSlabId { get; set; }
        [NotMapped]
        public double TaxPercentage { get; set; }
    }
    [Table("RestaurantMenuItem")]
    public class RestaurantMenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public int MenuHeadingId { get; set; }
        public string MenuItemName { get; set; }
        public double ItemPrice { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        
    }
}